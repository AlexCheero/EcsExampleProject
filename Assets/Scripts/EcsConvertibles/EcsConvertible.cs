using ECS;
using UnityEngine;

public abstract class EcsConvertible : MonoBehaviour
{
    protected int entityId;

    public bool IsAlive(EcsWorld world) => !world.IsDead(entityId);

    public virtual void Convert(EcsWorld world)
    {
        entityId = world.Create();
    }
}
