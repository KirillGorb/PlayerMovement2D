using UniRx;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class JumpSetting : ScriptableObject
    {
        [SerializeField] private float velocity;
        [SerializeField] private float velocityGravity;
        [SerializeField] private float jumpTime;
        [SerializeField] private float jumpImpulse;
        [SerializeField] private float maxGravity;
        [SerializeField] private float gravityDownMove;
        [SerializeField] private float dopTimeFly;

        public float Velocity => velocity;
        public float VelocityGravity => velocityGravity;
        public float JumpTime => jumpTime;
        public float JumpImpulse => jumpImpulse;
        public float MaxGravity => maxGravity;
        public float GravityDownMove => gravityDownMove;
        public float DopTimeFly => dopTimeFly;

        public BoolReactiveProperty IsCanJump { get; } = new BoolReactiveProperty(true);
    }
}