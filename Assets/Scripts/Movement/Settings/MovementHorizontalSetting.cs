using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class MovementHorizontalSetting : ScriptableObject
    {
        [SerializeField] private float speed;
        
        public ModificateLogic ModValueSpeed { get; } = new ModificateLogic();
        public ModificateLogic ModValueX { get; } = new ModificateLogic();

        public float Speed => speed + ModValueSpeed.GetMod;
        public BoolReactiveProperty IsCanMoveHorizontal { get; } = new BoolReactiveProperty(true);
    }
}