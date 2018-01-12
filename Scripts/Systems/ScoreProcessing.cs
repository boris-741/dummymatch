using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeopotamGroup.Ecs;
using UnityEngine.UI;

// public class ScoreProcessing : IEcsInitSystem, IEcsRunSystem  {

// 	[EcsWorld]
//     EcsWorld _world;
    
//     [EcsFilterInclude (typeof (ScoreComponent))]
//     EcsFilter _scoreFilter;


//     [EcsIndex (typeof (ScoreComponent))]
//     int _scoreId;

//     Text scoreui;


//     void IEcsInitSystem.Initialize () {
//         scoreui = GameObject.Find("Canvas/Text").GetComponent<Text>();
//         scoreui.text = string.Format("Score:{0}", 0);
//         _world.AddComponent<ScoreComponent>
        
//     }

//     void IEcsInitSystem.Destroy () { }

//     EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
//         return EcsRunSystemType.Update;
//     }

//     void IEcsRunSystem.Run () {
//         foreach (var scoreEntity in _scoreFilter.Entities) {
//             var score =_world.GetComponent<ScoreComponent>(scoreEntity, _scoreId);
//             scoreui.text = string.Format("Score:{0}", score.score);
//         }
//         // and remove all received events.
//         foreach (var entity in entities) {
//             _world.RemoveEntity (entity);
//         }
//     }
// }


public class ScoreProcessing : EcsReactSystem, IEcsInitSystem {
    [EcsWorld]
    EcsWorld _world;
    
    [EcsFilterInclude (typeof (ScoreComponent))]
    EcsFilter _scoreFilter;


    [EcsIndex (typeof (ScoreComponent))]
    int _scoreId;

    Text scoreui;
    int soreCount = 0;


    void IEcsInitSystem.Initialize () {
        scoreui = GameObject.Find("Canvas/Text").GetComponent<Text>();
        scoreui.text = FormatText();
        
    }

    void IEcsInitSystem.Destroy () { }

    string FormatText () {
        return string.Format ("Score: {0}", soreCount);
    }

    public override EcsFilter GetReactFilter () {
        return _scoreFilter;
    }

    public override EcsRunSystemType GetRunSystemType () {
        return EcsRunSystemType.Update;
    }

    public override EcsReactSystemType GetReactSystemType () {
        return EcsReactSystemType.OnAdd;
    }

    public override void RunReact (List<int> entities) {
        foreach (var scoreEntity in _scoreFilter.Entities) {
            var score =_world.GetComponent<ScoreComponent>(scoreEntity, _scoreId);
            soreCount += score.score;
            scoreui.text = FormatText();
        }
        // and remove all received events.
        foreach (var entity in entities) {
            _world.RemoveEntity (entity);
        }
    }
}

