using UnityEngine;

namespace UnityVsEcsPerformanceTest {
    [DefaultExecutionOrder (10)]
    public class UnityPostMeasure : MonoBehaviour {
        System.Diagnostics.Stopwatch _watch;

        public void Init (System.Diagnostics.Stopwatch watch) {
            _watch = watch;
        }

        void Update () {
            _watch.Stop ();
        }
    }
}