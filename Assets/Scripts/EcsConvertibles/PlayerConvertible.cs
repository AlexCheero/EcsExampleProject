using ECS;
using UnityEngine;

public class PlayerConvertible : EcsConvertible
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    public override void Convert(EcsWorld world)
    {
        base.Convert(world);

        world.AddComponent(entityId, transform);
        world.AddComponent(entityId, new PositionComponent { position = transform.position });
        world.AddComponent(entityId, new DirectionComponent { direction = transform.forward });
        world.AddComponent(entityId, new SpeedComponent { speed = _speed });
        world.AddComponent(entityId, new RotationSpeedComponent { speed = _rotationSpeed });
        
        world.AddTag<PlayerTag>(entityId);
        world.AddTag<GravityAffectedTag>(entityId);
    }
}
