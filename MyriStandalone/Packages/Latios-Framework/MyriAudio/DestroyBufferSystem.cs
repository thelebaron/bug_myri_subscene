using Unity.Entities;

namespace MyriAudio
{
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public class AudioDestroyCommandBufferSystem : EntityCommandBufferSystem
    {
    }
}