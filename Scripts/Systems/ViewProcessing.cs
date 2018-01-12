using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;


public class ViewProcessing : IEcsInitSystem, IEcsRunSystem  {

	[EcsWorld]
    EcsWorld _world;
    
    [EcsFilterInclude (typeof (ResComponent))]
    EcsFilter _resFilter;


    [EcsIndex (typeof (ResComponent))]
    int _resId;
    [EcsIndex (typeof (ResViewComponent))]
    int _viewId;
    [EcsIndex (typeof (ResMoveComponent))]
    int _moveId;


    void IEcsInitSystem.Initialize () {
        Transform maintr = GameObject.Find("Main").transform;
        foreach (var resEntity in _resFilter.Entities)
        {
            var res = _world.GetComponent<ResComponent>(resEntity, _resId);
            var view = _world.AddComponent<ResViewComponent>(resEntity);
            view.transform = GameObject.Instantiate(Resources.Load(GetResourcePath(res.type)) as GameObject).transform;
            view.transform.localPosition = new Vector3(res.x, res.y, 0);
            view.transform.parent = maintr;
        }
        
    }

    void IEcsInitSystem.Destroy () { }

    EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    void IEcsRunSystem.Run () {
        foreach (var resEntity in _resFilter.Entities) {
            var res =_world.GetComponent<ResComponent>(resEntity, _resId);
            var view = _world.GetComponent<ResViewComponent>(resEntity, _viewId);
            if(res.type != ResType.none)
            {
                var down = GetDownRes(res.x, res.y - 1);
                if(down != null && down.type == ResType.none)
                {
                    int downentity = GetDownEntity(res.x, res.y - 1);
                    if(downentity == -1)
                        Debug.Log("down entity -1");
                    var downres = _world.GetComponent<ResComponent>(downentity, _resId);
                    down.type = res.type;
                    res.type = ResType.none;
                    var downview = _world.GetComponent<ResViewComponent>( downentity, _viewId );
                    if(downview == null)
                        downview =_world.AddComponent<ResViewComponent>( downentity, _viewId );
                    downview.transform = view.transform;
                    var move = _world.GetComponent<ResMoveComponent>(downentity, _moveId);
                    if(move == null)
                        move =_world.AddComponent<ResMoveComponent>( downentity, _moveId );
                    Debug.Log("add for x="+_world.GetComponent<ResComponent>(downentity, _resId).x+" y="+_world.GetComponent<ResComponent>(downentity, _resId).y);
                    move.startx = res.x;
                    move.starty = res.y;
                    move.endx = downres.x;
                    move.endy = downres.y;
                    //_world.RemoveComponent<ResViewComponent>(resEntity, _viewId);
                    //_world.RemoveComponent<ResMoveComponent>(resEntity, _moveId);
                    Debug.Log("Remove for x="+_world.GetComponent<ResComponent>(resEntity, _resId).x+" y="+_world.GetComponent<ResComponent>(resEntity, _resId).y);
                    List<IEcsComponent> list = new List<IEcsComponent>();
                    _world.GetComponents(downentity, list);
                    Debug.Log("down comp count = "+list.Count);
                }
            }
        }
    }

    int GetDownEntity(int x, int y)
    {
        int down = -1;
        foreach (var resEntity in _resFilter.Entities){
            var res =_world.GetComponent<ResComponent>(resEntity, _resId);
            if(res.x == x && res.y == y)
                down = resEntity;
        }
        return down;
    }

    ResComponent GetDownRes(int x, int y)
    {
        ResComponent down = null;
        foreach (var resEntity in _resFilter.Entities){
            var res =_world.GetComponent<ResComponent>(resEntity, _resId);
            if(res.x == x && res.y == y)
                down = res;
        }
        return down;
    }

    string GetResourcePath(ResType restype)
    {
        string path = "";
        switch(restype)
        {
            case ResType.blocker:
                path = "Blocker";
            break;
            case ResType.piece0:
                path = "Piece0";
            break;
            case ResType.piece1:
                path = "Piece1";
            break;
            case ResType.piece2:
                path = "Piece2";
            break;
            case ResType.piece3:
                path = "Piece3";
            break;
            case ResType.piece4:
                path = "Piece4";
            break;
            case ResType.piece5:
                path = "Piece5";
            break;
        }
        return path;
    }
}


