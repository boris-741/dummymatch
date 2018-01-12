using LeopotamGroup.Ecs;
using System.Collections;
using UnityEngine;

public class GameStartup : MonoBehaviour {
    EcsWorld _world;

    void OnEnable () {
        _world = new EcsWorld ()
        .AddSystem (new TableProcessing ())
        .AddSystem (new ViewProcessing ())
        .AddSystem (new MoveProcessing ())
        .AddSystem (new InputProcessing ())
        .AddSystem (new RemoveProcessing ());
        _world.Initialize ();
    }

    void Update () {
        _world.RunUpdate ();
    }

    void OnDisable () {
        _world.Destroy ();
    }
}