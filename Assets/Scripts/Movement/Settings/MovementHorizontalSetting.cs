using UniRx;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class MovementHorizontalSetting : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float startSlow;

        public ModificateLogic ModValueSpeed { get; } = new();
        public ModificateLogic ModValueX { get; } = new();

        public float Speed => speed + ModValueSpeed.GetMod;
        public float Slow => startSlow;

        public Activator Activator { get; } = new(new BoolReactiveProperty(true));
    }
}