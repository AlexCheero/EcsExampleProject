
using UnityEngine;

//components
public struct PositionComponent { public Vector3 position; }
public struct DirectionComponent { public Vector3 direction; }
public struct SpeedComponent { public float speed; }
public struct RotationSpeedComponent { public float speed; }
public struct TargetComponent { public int targetId; }
public struct CameraComponent
{
    public float distanceToPlayer;
    public float heightAbovePlayer;
}

//tags
public struct PlayerTag { }
public struct GravityAffectedTag { }