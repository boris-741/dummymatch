using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;

public class InputProcessing : IEcsInitSystem, IEcsRunSystem {

    [EcsWorld]
    EcsWorld _world;
    
	[EcsFilterInclude (typeof (ResViewComponent))]
    EcsFilter _viewFilter;

    [EcsIndex (typeof (ResViewComponent))]
    int _viewId;
    [EcsIndex (typeof (ResComponent))]
    int _resId;
    [EcsIndex (typeof (ResMoveComponent))]
    int _moveId;

    void IEcsInitSystem.Initialize () {}

    void IEcsInitSystem.Destroy () {}

    EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    void IEcsRunSystem.Run () {
        if(Input.GetMouseButtonDown(0))   
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100);
            if (hit.collider != null) {
                Transform htr = hit.collider.transform;
                foreach (var viewEntity in _viewFilter.Entities) {
                    var view = _world.GetComponent<ResViewComponent> (viewEntity, _viewId);
                    var move = _world.GetComponent<ResMoveComponent> (viewEntity, _moveId);
                    if(view.transform == htr && move == null)
                    {
                        var res = _world.GetComponent<ResComponent>(viewEntity, _resId);
                        res.type = ResType.none;
                        GameObject.Destroy(view.transform.gameObject);
                        //_world.RemoveComponent<ResViewComponent>(viewEntity, _viewId);
                        break;
                    }
                }
            }
        }
    }
	
}
