using UnityEngine;

namespace UnityVsEcsPerformanceTest {
    [DefaultExecutionOrder (0)]
    public class UnityComponent : MonoBehaviour {
        public int Counter;

        void Update () {
            Counter = (Counter + 1) % 65536;
        }
    }
}