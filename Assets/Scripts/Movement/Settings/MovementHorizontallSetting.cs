using UnityEngine;
using UniRx;

namespace Play.Movement.Setting
{
    public class MovementHorizontallSetting : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxClimbAngle;

        public float Speed => speed;
        public float MaxClimbAngle => maxClimbAngle;
        public BoolReactiveProperty IsCanMoveHorizontal { get; } = new BoolReactiveProperty(true);
    }
}