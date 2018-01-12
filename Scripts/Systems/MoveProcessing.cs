using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;

public class MoveProcessing : IEcsInitSystem, IEcsRunSystem  {

    [EcsWorld]
    EcsWorld _world;

	[EcsFilterInclude (typeof (ResMoveComponent))]
    EcsFilter _moveFilter;

    [EcsIndex (typeof (ResComponent))]
    int _resId;
    [EcsIndex (typeof (ResViewComponent))]
    int _viewId;
    [EcsIndex (typeof (ResMoveComponent))]
    int _moveId;

    void IEcsInitSystem.Initialize () {
    }

    void IEcsInitSystem.Destroy () { }

    EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    void IEcsRunSystem.Run () {
        foreach (var moveEntity in _moveFilter.Entities) {
            var res = _world.GetComponent<ResComponent>(moveEntity, _resId);
            var move = _world.GetComponent<ResMoveComponent>(moveEntity, _moveId);
            var view = _world.GetComponent<ResViewComponent>(moveEntity, _viewId);
            move.curTime += Time.deltaTime;
            move.curcycle += 1;
            Vector3 pos = Vector2.Lerp(new Vector2(move.startx, move.starty), new Vector2(move.endx, move.endy), move.curTime/move.tmMove);
            if(view != null && view.transform != null)
            {
                view.transform.position = pos;
                if(move.curcycle > move.cyclemove)
                {
                    view.transform.position = new Vector2(move.endx, move.endy);
                    //_world.RemoveComponent<ResMoveComponent>(moveEntity, _moveId);
                }
            }
            else
            {
                List<IEcsComponent> list = new List<IEcsComponent>();
                _world.GetComponents(moveEntity, list);
                if(view == null)
                    Debug.Log("view null for x="+res.x+" y="+res.y+" down comp count = "+list.Count);
                else
                    Debug.Log("transform null for x="+res.x+" y="+res.y+" down comp count = "+list.Count);
            }
        }
    }

}


