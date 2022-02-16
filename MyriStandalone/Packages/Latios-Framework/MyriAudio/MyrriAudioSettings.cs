using Unity.Entities;
using UnityEngine;

namespace MyriAudio
{
    public class MyrriAudioSettings : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Latios.Myri.AudioSettings
            {
                safetyAudioFrames             = 2,
                audioFramesPerUpdate          = 1,
                lookaheadAudioFrames          = 0,
                logWarningIfBuffersAreStarved = false
            });
        }
    }
}