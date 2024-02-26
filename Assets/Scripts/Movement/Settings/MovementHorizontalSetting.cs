using UniRx;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class MovementHorizontalSetting : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float startSlow;

        public ModificateLogic ModValueSpeed { get; } = new ModificateLogic();
        public ModificateLogic ModValueX { get; } = new ModificateLogic();

        public float Speed => speed + ModValueSpeed.GetMod;
        public float Slow => startSlow;

        public Activator Activator { get; } = new Activator(new BoolReactiveProperty(true));
    }
}