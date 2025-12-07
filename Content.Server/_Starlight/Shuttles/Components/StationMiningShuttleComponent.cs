using Robust.Shared.Utility;

namespace Content.Server._Starlight.Shuttles.Components;

/// <summary>
/// GridSpawnComponent but for cargo shuttles
/// <remarks>
/// This exists so we don't need to make 1 change to GridSpawn for every single station's unique shuttles.
/// </remarks>
/// </summary>
[RegisterComponent]
public sealed partial class StationMiningShuttleComponent : StationRandomShuttleBaseComponent
{
    public StationMiningShuttleComponent()
    {
        BasePath = new ResPath("/Maps/_Starlight/Shuttles/Mining/");
        HasSizeClassFolder = true;
    }
}