using Content.Shared.Inventory;
using Robust.Shared.GameStates;

namespace Content.Shared.Radio.Components;

/// <summary>
/// This component relays radio messages to the parent entity's chat when equipped.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class HeadsetComponent : Component
{
    [DataField, AutoNetworkedField]
    public bool Enabled = true;

    [DataField, AutoNetworkedField]
    public bool IsEquipped = false;

    [DataField, AutoNetworkedField]
    public SlotFlags RequiredSlot = SlotFlags.EARS;

    /// <summary>
    ///     RMC14 determines how much larger the radio message size will be.
    /// </summary>
    [DataField]
    public int? RadioTextIncrease { get; set; } = 0;
}
