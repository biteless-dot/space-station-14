

using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

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

    /// <summary>
    /// If the upgradeable armor has a sprite state corresponding to this string,
    /// set that state to visible.
    /// </summary>
    [DataField]
    public string? RevealLayerName;

    /// <summary>
    /// If set, will apply the specified layer to ALL armor pieces.
    /// TODO: Make this discriminate between Vox and other race-specific sprites.
    /// </summary>
    /// <remarks>
    /// The reason for specifying sprite-location is for clothing with
    /// toggleable components, such as hardsuits.
    [DataField]
    public Dictionary<SpriteLocation, SpriteSpecifier>? OverlaySprites = new();
}

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

public enum SpriteLocation
{
    Universal = 0,
    Back = 1,
    Belt = 2,
    Ears = 3,
    Eyes = 4,
    Hands = 5,
    Head = 6,
    Mask = 7,
    Neck = 8,
    OuterClothing = 9,
    Shoes = 10,
    Under = 11,
}