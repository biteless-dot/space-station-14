

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using Content.Shared.Tag;
using Content.Shared.Weapons.Ranged.Upgrades.Components;
using Content.Shared.Whitelist;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Armor.Upgrades.Components;

[RegisterComponent, NetworkedComponent, Access(typeof(ArmorUpgradeSystem))]
public sealed partial class UpgradeableArmorComponent : Component
{
    public UpgradeableArmorComponent()
    {
        if (UpgradesPerTag == null && Whitelist.Tags != null)
        {
            UpgradesPerTag = new();
            foreach (ProtoId<TagPrototype> tag in Whitelist.Tags)
            {
                UpgradesPerTag.Add(tag, 1);
            }
        }
    }
    [DataField]
    public EntityWhitelist Whitelist = new();

    [DataField]
    public SoundSpecifier? InsertSound = new SoundPathSpecifier("/Audio/Effects/thunk.ogg");

    /// <summary>
    /// The name of the container this upgrade goes into.
    /// </summary>
    [DataField]
    public string UpgradesContainerId = "upgrades";

    /// <summary>
    /// How many upgrades of the same tag can be fitted.
    /// By default, this is set to 1 on every whitelisted tag.
    /// </summary>
    [DataField]
    public Dictionary<ProtoId<TagPrototype>, int> UpgradesPerTag = new();

}