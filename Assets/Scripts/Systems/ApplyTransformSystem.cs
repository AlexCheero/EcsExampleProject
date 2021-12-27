using ECS;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class ApplyTransformSystem : EcsSystem
    {
        private int _gravityFilterId;
        private int _nonGraityFilterId;

        public ApplyTransformSystem(EcsWorld world)
        {
            var gravityIncludes = new BitMask(Id<PositionComponent>(), Id<DirectionComponent>(), Id<Transform>());
            var gravityExcludes = new BitMask(Id<GravityAffectedTag>());

            _gravityFilterId = world.RegisterFilter(in gravityIncludes, in gravityExcludes);

            var nonGravityIncludes = new BitMask(Id<PositionComponent>(), Id<DirectionComponent>()
                , Id<Transform>(), Id<GravityAffectedTag>());
            _nonGraityFilterId = world.RegisterFilter(in nonGravityIncludes);
        }

        public override void Tick(EcsWorld world)
        {
            world.GetFilter(_gravityFilterId).Iterate((entities, count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    var position = world.GetComponent<PositionComponent>(entities[i]).position;
                    var transform = world.GetComponent<Transform>(entities[i]);
                    var direction = world.GetComponent<DirectionComponent>(entities[i]).direction;

                    transform.position = position;
                    transform.forward = direction;
                }
            });

            world.GetFilter(_nonGraityFilterId).Iterate((entities, count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    var position = world.GetComponent<PositionComponent>(entities[i]).position;
                    var transform = world.GetComponent<Transform>(entities[i]);
                    var direction = world.GetComponent<DirectionComponent>(entities[i]).direction;

                    position.y = transform.position.y;
                    transform.position = position;
                    transform.forward = direction;
                }
            });
        }
    }
}
