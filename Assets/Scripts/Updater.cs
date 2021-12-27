using Assets.Scripts.Systems;
using ECS;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystem[] _systems;

    void Start()
    {
        _world = new EcsWorld();

        var initSystems = new EcsSystem[] { new InitCameraSystem(_world) };
        _systems = new EcsSystem[]
        {
            new PlayerMovementSystem(_world),
            new PlayerDirectionSystem(_world),
            new CameraFollowSystem(_world),

            //should be after all systems that changes position components
            new ApplyTransformSystem(_world)
        };

        //create entities only after registring systems
        var convertibles = FindObjectsOfType<EcsConvertible>();
        foreach (var convertible in convertibles)
            convertible.Convert(_world);

        //init systems should tick only once, after converting game objects to initial entities
        for (int i = 0; i < initSystems.Length; i++)
            initSystems[i].Tick(_world);
    }

    void Update()
    {
        for (int i = 0; i < _systems.Length; i++)
            _systems[i].Tick(_world);
    }
}
