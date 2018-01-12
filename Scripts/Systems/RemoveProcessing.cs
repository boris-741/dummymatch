using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;

public class RemoveProcessing : IEcsRunSystem  {

    [EcsWorld]
    EcsWorld _world;

	[EcsFilterInclude (typeof (ResComponent))]
    EcsFilter _resFilter;

    [EcsFilterInclude (typeof (ResMoveComponent))]
    EcsFilter _moveFilter;


    [EcsIndex (typeof (ResComponent))]
    int _resId;
    [EcsIndex (typeof (ResViewComponent))]
    int _viewId;
    [EcsIndex (typeof (ResMoveComponent))]
    int _moveId;

    EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    void IEcsRunSystem.Run () {
        foreach (var resEntity in _resFilter.Entities) {
            var res =_world.GetComponent<ResComponent>(resEntity, _resId);
            if(res.type == ResType.none)
            {
                _world.RemoveComponent<ResViewComponent>(resEntity, _viewId);
                _world.RemoveComponent<ResMoveComponent>(resEntity, _moveId);
            }
        }

        foreach (var moveEntity in _moveFilter.Entities) {
            var move =_world.GetComponent<ResMoveComponent>(moveEntity, _moveId);
            if(move.curcycle > move.cyclemove)
            {
                _world.RemoveComponent<ResMoveComponent>(moveEntity, _moveId);
            }
        }
    }

}