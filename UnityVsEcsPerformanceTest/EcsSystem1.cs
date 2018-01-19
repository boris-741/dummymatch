using LeopotamGroup.Ecs;

namespace UnityVsEcsPerformanceTest {
    public class EcsSystem1 : IEcsRunSystem {
        [EcsWorld]
        EcsWorld _world;

        [EcsFilterInclude (typeof (EcsComponent1))]
        EcsFilter _filter;

        [EcsIndex (typeof (EcsComponent1))]
        int _componentId;

        EcsRunSystemType IEcsRunSystem.GetRunSystemType () {
            return EcsRunSystemType.Update;
        }

        void IEcsRunSystem.Run () {
            for (var i = 0; i < _filter.Entities.Count; i++) {
                var c1 = _world.GetComponent<EcsComponent1> (_filter.Entities[i], _componentId);
                c1.Counter = (c1.Counter + 1) % 65536;
            }
        }
    }
}