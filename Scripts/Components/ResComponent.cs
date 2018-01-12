using LeopotamGroup.Ecs;
using UnityEngine;

sealed class ResComponent : IEcsComponent {
    public ResType type;
    public int x;
    public int y;
    //public Transform Transform;
}
