using System.Collections.Generic;
using LeopotamGroup.Ecs;
using UnityEngine;

sealed class TableProcessing : EcsReactSystem, IEcsInitSystem {
    [EcsWorld]
    EcsWorld _world;

    [EcsFilterInclude (typeof (ResComponent))]
    EcsFilter _resFilter;

    [EcsIndex (typeof (ResComponent))]
    int _resId;

    int countx = 9;
    int county = 9;

    void IEcsInitSystem.Initialize () {
        countx = 9;
        county = 9;
        CreateTable();
    }

    void CreateTable()
    {
        for(int x=0; x<countx; x++)
        {
            for(int y = 0; y<county; y++)
            {
                var res = _world.AddComponent<ResComponent> (_world.CreateEntity ());
                res.x = x;
                res.y = y;
                res.type = (ResType)Random.Range((int)ResType.blocker, (int)ResType.piece5);
            }
        }
    }

    void IEcsInitSystem.Destroy () { }

    public override EcsFilter GetReactFilter () {
        return _resFilter;
    }

    public override EcsRunSystemType GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    public override EcsReactSystemType GetReactSystemType () {
        return EcsReactSystemType.OnAdd;
    }

    public override void RunReact (List<int> entities) {
        // // no need to repeat update for all events - we can do it once.
        // foreach (var resEntity in _resFilter.Entities) {
        //     var score = _world.GetComponent<ResComponent> (resEntity, _resId);

        // }

        // // and remove all received events.
        // foreach (var entity in entities) {
        //     _world.RemoveEntity (entity);
        // }
    }
}