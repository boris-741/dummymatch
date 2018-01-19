using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeopotamGroup.Ecs;

public class Processing1 : IEcsInitSystem, IEcsRunSystem  {

	[EcsWorld]
    EcsWorld _world;
    
    [EcsFilterInclude (typeof (MainComponent))]
    EcsFilter _mainFilter;


    [EcsIndex (typeof (MainComponent))]
    int _mainId;
    [EcsIndex (typeof (Comp1Cpmponent))]
    int _comp1Id;

    int cyclenum = 0;
    System.Diagnostics.Stopwatch _sw = new System.Diagnostics.Stopwatch ();
    Text text;
    Text listtext;
    Text gotext;
    GameObject go;
    List<GameObject> listgo;
    List<MainComponentItem> list;

    void IEcsInitSystem.Initialize () {
        list = new List<MainComponentItem>();
        listgo = new List<GameObject>();
        text = GameObject.Find("Canvas/Text").GetComponent<Text>();
        listtext = GameObject.Find("Canvas/ListText").GetComponent<Text>();
        gotext = GameObject.Find("Canvas/GOText").GetComponent<Text>();
        go = GameObject.Find("Main");
        _mainFilter.OnEntityAdded += OnAddEntity;
        _mainFilter.OnEntityRemoved += OnRemoveEntity;

        GameObject tgo;
        for(int i=0; i<50000; i++)
        {
            tgo = new GameObject("test");
            tgo.AddComponent<TestScr>();
            listgo.Add(tgo);

        }


        // for(int i=0 ; i<10; i++)
        // {
        //     var res = _world.AddComponent<MainComponent>(_world.CreateEntity());
        //     res.id = i + 1;
        // }
    }

    void OnAddEntity(int entity, int componentId)
    {
        MainComponent comp = _world.GetComponent<MainComponent>(entity, componentId);
        MainComponentItem item = new MainComponentItem();
        item.entityid = entity;
        item.obj = comp;
        list.Add(item);
    }

    void OnRemoveEntity(int entity, int componentId)
    {
        for(int i=0; i<list.Count; i++)
        {
            if(list[i].entityid == entity)
            {
                list.RemoveAt(i);
                break;
            }
        }
    }

    void IEcsInitSystem.Destroy () { }

    EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    void IEcsRunSystem.Run () {
        _sw.Reset();
        _sw.Start();
        MainComponent res;
        for(int i=0; i<_mainFilter.Entities.Count; i++)
        {
            res =_world.GetComponent<MainComponent>(_mainFilter.Entities[i], _mainId);
        }
        _sw.Stop();
        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("Time:{0}", _sw.Elapsed.Milliseconds);
        text.text = elapsedTime;
        _sw.Reset();
        _sw.Start();
        for(int i=0; i<list.Count; i++)
        {
            res = list[i].obj;
        }
        _sw.Stop();
        elapsedTime = String.Format("List:{0} Time:{1}", list.Count, _sw.Elapsed.Milliseconds);
        listtext.text = elapsedTime;
        _sw.Reset();
        _sw.Start();
        TestScr scr;//AddRemoveStart scr;
        for(int i=0; i<listgo.Count; i++)
        {
            scr = listgo[i].GetComponent<TestScr>();//go.GetComponent<AddRemoveStart>();
        }
        _sw.Stop();
        elapsedTime = String.Format("GO:{0} Time:{1}", list.Count, _sw.Elapsed.Milliseconds);
        gotext.text = elapsedTime;

        // foreach (var mainEntity in _mainFilter.Entities) {
        //     res =_world.GetComponent<MainComponent>(mainEntity, _mainId);
        //     //_world.RemoveComponent<Comp1Cpmponent>(mainEntity, _comp1Id);

        //     // if(cyclenum == 1)
        //     // {
        //     //     //_world.AddComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     // }
        //     // else
        //     // {
        //     //     var comp1 =_world.GetComponent<Comp1Cpmponent>(mainEntity, _comp1Id); 
        //     //     //if(comp1 == null) Debug.Log("have't comp1 on id="+res.id);
        //     // }
        //     // if(cyclenum == 0)
        //     // {
        //     //     _world.AddComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     // }
        //     // else if(cyclenum == 1)
        //     // {
        //     //     _world.RemoveComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     //     _world.AddComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     // }
        //     // else
        //     // {
        //     //     var comp1 =_world.GetComponent<Comp1Cpmponent>(mainEntity, _comp1Id);
        //     //     if(comp1 == null) Debug.Log("have't comp1 on id="+res.id);
        //     // }
        // }
        //cyclenum ++;
    }
}