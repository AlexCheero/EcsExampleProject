
using ECS;
using UnityEngine;

public class CameraConvertible : EcsConvertible
{
    [SerializeField]
    private float _distanceToPlayer;
    [SerializeField]
    private float _heightAbovePlayer;

    public override void Convert(EcsWorld world)
    {
        base.Convert(world);

        world.AddComponent(entityId, transform);
        world.AddComponent(entityId, new PositionComponent { position = transform.position });
        world.AddComponent(entityId, new DirectionComponent { direction = transform.forward });
        world.AddComponent<TargetComponent>(entityId);
        world.AddComponent(entityId,
            new CameraComponent
            {
                distanceToPlayer = _distanceToPlayer,
                heightAbovePlayer = _heightAbovePlayer
            });
    }
}
