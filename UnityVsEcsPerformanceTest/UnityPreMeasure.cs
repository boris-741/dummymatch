using UnityEngine;

namespace UnityVsEcsPerformanceTest {
    [DefaultExecutionOrder (-10)]
    public class UnityPreMeasure : MonoBehaviour {
        System.Diagnostics.Stopwatch _watch;

        public void Init (System.Diagnostics.Stopwatch watch) {
            _watch = watch;
        }

        void Update () {
            _watch.Reset ();
            _watch.Start ();
        }
    }
}