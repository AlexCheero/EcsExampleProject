using ECS;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class CameraFollowSystem : EcsSystem
    {
        private int _filterId;

        public CameraFollowSystem(EcsWorld world)
        {
            var includes = new BitMask(Id<PositionComponent>(), Id<DirectionComponent>()
                , Id<TargetComponent>(), Id<CameraComponent>());

            _filterId = world.RegisterFilter(in includes);
        }

        public override void Tick(EcsWorld world)
        {
            int cameraEntityId = world.GetFilter(_filterId)[0];
            ref var position = ref world.GetComponent<PositionComponent>(cameraEntityId).position;
            ref var direction = ref world.GetComponent<DirectionComponent>(cameraEntityId).direction;
            var cameraComp = world.GetComponent<CameraComponent>(cameraEntityId);
            var targetId = world.GetComponent<TargetComponent>(cameraEntityId).targetId;
            var targetPosition = world.GetComponent<PositionComponent>(targetId).position;
            var targetDirection = world.GetComponent<DirectionComponent>(targetId).direction;

            direction = targetDirection;
            position = targetPosition - (direction * cameraComp.distanceToPlayer) +
                (Vector3.up * cameraComp.heightAbovePlayer);

            /*
            world.GetFilter(_filterId).Iterate((entities, count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    
                }
            });
            */
        }
    }
}
