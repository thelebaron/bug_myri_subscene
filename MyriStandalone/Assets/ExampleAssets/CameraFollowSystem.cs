using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ExampleAssets
{
    public class CameraFollowSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<CameraFollower.CameraFollowerComponent>();
        }

        protected override void OnUpdate()
        {
            var camera         = Camera.main;
            var cameraPosition = camera.transform.position;
            var cameraRotation = camera.transform.rotation;
            var cameraFollower = GetSingletonEntity<CameraFollower.CameraFollowerComponent>();
            
            Entities
                .WithAll<CameraFollower.CameraFollowerComponent>()
                .ForEach((Entity entity, ref Translation translation, ref Rotation rotation) =>
                {
                    translation.Value = cameraPosition;
                    rotation.Value    = cameraRotation;
                }).Run();
            
        }
    }
}