using LeopotamGroup.Ecs;
using UnityEngine;

namespace UnityVsEcsPerformanceTest {
    [DefaultExecutionOrder (200)]
    public class Startup : MonoBehaviour, IEcsInitSystem {
        [EcsIndex (typeof (EcsComponent1))]
        int _componentId;

        const int ITERATIONS = 100000;

        UnityPreMeasure _unityPre;

        UnityPostMeasure _unityPost;

        System.Diagnostics.Stopwatch _ecsWatch = new System.Diagnostics.Stopwatch ();

        System.Diagnostics.Stopwatch _unityWatch = new System.Diagnostics.Stopwatch ();

        EcsWorld _world;

        string _unityResult = "";

        string _ecsResult = "";

        void Start () {
            InitUnityTest ();
            InitEcsTest ();
        }

        void InitEcsTest () {
            _world = new EcsWorld ()
                .AddSystem (this)
                .AddSystem (new EcsSystem1 ());
            _world.Initialize ();
        }

        void InitUnityTest () {
            _unityPre = gameObject.AddComponent<UnityPreMeasure> ();
            _unityPost = gameObject.AddComponent<UnityPostMeasure> ();
            _unityPre.Init (_unityWatch);
            _unityPost.Init (_unityWatch);

            for (var i = 0; i < ITERATIONS; i++) {
                var go = new GameObject ();
                go.AddComponent<UnityComponent> ();
            }
        }

        void Update () {
            _ecsWatch.Reset ();
            _ecsWatch.Start ();
            _world.RunUpdate ();
            _ecsWatch.Stop ();

            _unityResult = string.Format ("unity: {0}ms", _unityWatch.Elapsed.Milliseconds);
            _ecsResult = string.Format ("ecs: {0}ms", _ecsWatch.Elapsed.Milliseconds);
        }

        void OnGUI () {
            GUILayout.Label (_unityResult);
            GUILayout.Label (_ecsResult);
        }

        void IEcsInitSystem.Initialize () {
            for (var i = 0; i < ITERATIONS; i++) {
                _world.CreateEntityWith<EcsComponent1> (_componentId);
            }
        }

        void IEcsInitSystem.Destroy () { }
    }
}