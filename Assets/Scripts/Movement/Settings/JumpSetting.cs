using UniRx;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class JumpSetting : ScriptableObject
    {
        [SerializeField] private float velocity;
        [SerializeField] private float jumpTime;
        [SerializeField] private float jumpImpulse;
        [SerializeField] private float gravityDownMove;
        [SerializeField] private float dopTimeFly;
        [SerializeField] private float poolTimeClick;

        public ModificateLogic ModValueY { get; } = new ModificateLogic();

        public float PoolTimeClick => poolTimeClick;
        public float Velocity => velocity;
        public float JumpTime => jumpTime;
        public float JumpImpulse => jumpImpulse;
        public float GravityDownMove => gravityDownMove;
        public float DopTimeFly => dopTimeFly;

        public Activator Activator { get; } = new Activator(new BoolReactiveProperty(true));
    }
}