

using System.Linq;
using Content.Shared.Administration.Logs;
using Content.Shared.Armor.Upgrades.Components;
using Content.Shared.Database;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.Tag;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Whitelist;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;

namespace Content.Shared.Armor.Upgrades;

public sealed class ArmorUpgradeSystem : EntitySystem
{
    [Dependency] private readonly ISharedAdminLogManager _adminLog = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly SharedArmorSystem _armor = default!;
    [Dependency] private readonly EntityWhitelistSystem _entityWhitelist = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    /// <inheritdoc/>

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<UpgradeableArmorComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<UpgradeableArmorComponent, AfterInteractUsingEvent>(OnAfterInteractUsing);
    }

    private void OnAfterInteractUsing(Entity<UpgradeableArmorComponent> ent, ref AfterInteractUsingEvent args)
    {
        if (args.Handled || !args.CanReach || !TryComp<ArmorUpgradeComponent>(args.Used, out var upgradeComponent))
            return;

        if (GetUpgradesOfTag(upgradeComponent.Tag, ent) >= ent.Comp.UpgradesPerTag[upgradeComponent.Tag])
        {
            _popup.PopupPredicted(Loc.GetString("upgradeable-armor-popup-upgrade-limit"), ent, args.User);
            return;
        }

        if (_entityWhitelist.IsWhitelistFail(ent.Comp.Whitelist, args.Used))
            return;

        _audio.PlayPredicted(ent.Comp.InsertSound, ent, args.User);
        _popup.PopupClient(Loc.GetString("armor-upgrade-popup-insert", ("upgrade", args.Used), ("armor", ent.Owner)), args.User);
        args.Handled = _container.Insert(args.Used, _container.GetContainer(ent, ent.Comp.UpgradesContainerId));

        _adminLog.Add(LogType.Action, LogImpact.Low, $"{ToPrettyString(args.User):player} inserted armor upgrade {ToPrettyString(args.Used)} into {ToPrettyString(ent.Owner)}.");
    }

    private void OnInit(Entity<UpgradeableArmorComponent> ent, ref ComponentInit args)
    {
        _container.EnsureContainer<Container>(ent, ent.Comp.UpgradesContainerId);
    }

    /// <summary>
    /// Gets the entities inside the armor's upgrade container.
    /// </summary>
    public HashSet<Entity<ArmorUpgradeComponent>> GetCurrentUpgrades(Entity<UpgradeableArmorComponent> ent)
    {
        if (!_container.TryGetContainer(ent, ent.Comp.UpgradesContainerId, out var container))
            return new HashSet<Entity<ArmorUpgradeComponent>>();

        var upgrades = new HashSet<Entity<ArmorUpgradeComponent>>();
        foreach (var contained in container.ContainedEntities)
        {
            if (TryComp<ArmorUpgradeComponent>(contained, out var upgradeComp))
                upgrades.Add((contained, upgradeComp));
        }

        return upgrades;
    }

    public int GetUpgradesOfTag(ProtoId<TagPrototype> tag, Entity<UpgradeableArmorComponent> ent)
    {
        HashSet<Entity<ArmorUpgradeComponent>> UpgradeSet = GetCurrentUpgrades(ent);
        int count = 0;
        foreach (var upgrade in UpgradeSet)
        {
            if (upgrade.Comp.Tag == tag)
            {
                ++count;
            }
        }

        return count;
    }
}