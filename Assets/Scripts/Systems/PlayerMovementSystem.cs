
using ECS;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class PlayerMovementSystem : EcsSystem
    {
        private int _filterId;

        public PlayerMovementSystem(EcsWorld world)
        {
            var includes = new BitMask(Id<PositionComponent>(), Id<DirectionComponent>()
                , Id<SpeedComponent>(), Id<PlayerTag>());

            _filterId = world.RegisterFilter(in includes);
        }

        public override void Tick(EcsWorld world)
        {
            world.GetFilter(_filterId).Iterate((entities, count) =>
            {
                for (int i = 0; i < count; i++)
                {
                    ref var position = ref world.GetComponent<PositionComponent>(entities[i]).position;
                    var forward = world.GetComponent<DirectionComponent>(entities[i]).direction;
                    var speed = world.GetComponent<SpeedComponent>(entities[i]).speed;
                    var right = Vector3.Cross(Vector3.up, forward);

                    if (Input.GetKey("w"))
                        position += forward * Time.deltaTime * speed;
                    if (Input.GetKey("s"))
                        position -= forward * Time.deltaTime * speed;
                    if (Input.GetKey("d"))
                        position += right * Time.deltaTime * speed;
                    if (Input.GetKey("a"))
                        position -= right * Time.deltaTime * speed;
                }
            });
        }
    }
}