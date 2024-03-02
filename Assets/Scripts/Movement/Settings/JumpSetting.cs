using UniRx;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class JumpSetting : ScriptableObject
    {
        [SerializeField] private float velocity;
        [SerializeField] private float jumpImpulse;
        [SerializeField] private float dopHight;
        [SerializeField] private float dopJumpTime;
        [SerializeField] private int countDopJump;
        [SerializeField] private float gravityDownMove;
        [SerializeField] private float poolTimeClick;

        public ModificateLogic ModValueY { get; } = new();

        public float PoolTimeClick => poolTimeClick;
        public float JumpImpulse => jumpImpulse;
        public float DopHight => dopHight;
        public float DopJumpTime => dopJumpTime;
        public int CountDopJump => countDopJump;
        public float Velocity => velocity;
        public float GravityDownMove => gravityDownMove;

        public Activator Activator { get; } = new(new BoolReactiveProperty(true));
    }
}