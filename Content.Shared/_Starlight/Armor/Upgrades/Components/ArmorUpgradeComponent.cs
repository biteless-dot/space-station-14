

using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Armor.Upgrades.Components;

/// <summary>
/// Used to denote compatibility with <see cref="UpgradeableArmorComponent"/>. Actual behavior denoted in other components.
/// </summary>

[RegisterComponent, NetworkedComponent, Access(typeof(ArmorUpgradeSystem))]
public sealed partial class ArmorUpgradeComponent : Component
{
    /// <summary>
    /// Tags used to ensure mutually exclusive upgrades and duplicates are not stacked.
    /// </summary>
    [DataField]
    public List<ProtoId<TagPrototype>> Tags = new();

    /// <summary>
    /// Markup added to the item when this upgrade is applied.
    /// </summary>
    [DataField]
    public LocId ExamineText;

    /// <summary>
    /// Markup added to the title of the item when this upgrade is applied.
    /// </summary>
    [DataField]
    public TitleChangeType? TitleChanges;
}

[RegisterComponent]
[DataDefinition]
public sealed partial class TitleChangeType
{
    /// <summary>
    /// If indicated, will be added after the items title, after a comma.
    /// </summary>
    [DataField]
    public string? Suffix = null;

    /// <summary>
    /// If indicated, will be added before the title.
    /// </summary>
    [DataField]
    public string? Prefix = null;
}