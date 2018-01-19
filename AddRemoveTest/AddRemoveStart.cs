using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;

public class AddRemoveStart : MonoBehaviour {
    EcsWorld _world;

    void OnEnable () {
        _world = new EcsWorld ()
        .AddSystem (new Processing1 ())
        .AddSystem (new Processing2 ());
        _world.Initialize ();
    }

    void Update () {
        _world.RunUpdate ();
    }

    void OnDisable () {
        _world.Destroy ();
    }
}