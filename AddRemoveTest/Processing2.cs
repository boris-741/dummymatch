
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;

public class Processing2 : IEcsInitSystem, IEcsRunSystem  {

	[EcsWorld]
    EcsWorld _world;
    
    [EcsFilterInclude (typeof (MainComponent))]
    EcsFilter _mainFilter;


    [EcsIndex (typeof (MainComponent))]
    int _mainId;
    [EcsIndex (typeof (Comp1Cpmponent))]
    int _comp1Id;

    int cyclenum = 0;

    void IEcsInitSystem.Initialize () {
        for(int i=0 ; i<50000; i++) 
        {
            var res = _world.AddComponent<MainComponent>(_world.CreateEntity());
            res.id = i + 1;
        }
    }

    void IEcsInitSystem.Destroy () { }

    EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    void IEcsRunSystem.Run () {
        MainComponent res;
        for(int i=0; i<_mainFilter.Entities.Count; i++)
        {
            res =_world.GetComponent<MainComponent>(_mainFilter.Entities[i], _mainId);
        }
        // foreach (var mainEntity in _mainFilter.Entities) {
        //     res =_world.GetComponent<MainComponent>(mainEntity, _mainId);
        //     // _world.AddComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     // if(cyclenum == 0)
        //     // {
        //     //     //_world.AddComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     // }
        //     // else if(cyclenum == 1)
        //     // {
        //     //     //_world.RemoveComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     // }
        // }
        //cyclenum ++;
    }
}