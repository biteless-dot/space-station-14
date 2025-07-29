

using Content.Shared.Administration.Logs;
using Content.Shared.Armor.Upgrades.Components;
using Content.Shared.Popups;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Whitelist;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Containers;

namespace Content.Shared.Armor.Upgrades;

public sealed class ArmorUpgradeSystem : EntitySystem
{
    [Dependency] private readonly ISharedAdminLogManager _adminLog = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly SharedGunSystem _gun = default!;
    [Dependency] private readonly EntityWhitelistSystem _entityWhitelist = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    /// <inheritdoc/>

    public override void Initialize()
    {
        base.Initialize();

    }
}