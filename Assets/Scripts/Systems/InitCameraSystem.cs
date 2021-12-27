using ECS;

namespace Assets.Scripts.Systems
{
    class InitCameraSystem : EcsSystem
    {
        private int _camerafilterId;
        private int _playerfilterId;

        public InitCameraSystem(EcsWorld world)
        {
            //it is better to use one of existing in update systems filters,
            //to avoid unnecessary filter registration
            //so this filter duplicated from CameraFollowSystem even if not all the components from it
            //used in initialization
            var cameraIncludes = new BitMask(Id<PositionComponent>(), Id<DirectionComponent>()
                , Id<TargetComponent>(), Id<CameraComponent>());

            _camerafilterId = world.RegisterFilter(in cameraIncludes);

            //copied from PlayerMovementSystem
            var playerIncludes = new BitMask(Id<PositionComponent>(), Id<DirectionComponent>()
                , Id<SpeedComponent>(), Id<PlayerTag>());

            _playerfilterId = world.RegisterFilter(in playerIncludes);
        }

        public override void Tick(EcsWorld world)
        {
            int cameraEntityId = world.GetFilter(_camerafilterId)[0];
            int playerId = world.GetFilter(_playerfilterId)[0];

            world.GetComponent<TargetComponent>(cameraEntityId).targetId = playerId;
        }
    }
}
