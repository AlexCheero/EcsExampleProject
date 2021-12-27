using ECS;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    class PlayerDirectionSystem : EcsSystem
    {
        private int _filterId;

        public PlayerDirectionSystem(EcsWorld world)
        {
            var includes = new BitMask(Id<DirectionComponent>(), Id<RotationSpeedComponent>(), Id<PlayerTag>());

            _filterId = world.RegisterFilter(in includes);
        }

        public override void Tick(EcsWorld world)
        {
            world.GetFilter(_filterId).Iterate((entities, count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    ref var direction = ref world.GetComponent<DirectionComponent>(entities[i]).direction;
                    var rotationSpeed = world.GetComponent<RotationSpeedComponent>(entities[i]).speed;

                    if (Input.GetKey("e"))
                        direction = (Quaternion.Euler(0, Time.deltaTime * rotationSpeed, 0) * direction).normalized;
                    if (Input.GetKey("q"))
                        direction = (Quaternion.Euler(0, -Time.deltaTime * rotationSpeed, 0) * direction).normalized;
                }
            });
        }
    }
}
