using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class MovementHorizontalSetting : ScriptableObject
    {
        [SerializeField] private float speed;
        
        private float _modif = 0;

        public void AddModifSpeed(float mod) => _modif += mod;
        public void RemoveModifSpeed(float mod) => _modif -= mod;
        public void ClearModifSpeed(float mod) => _modif = 0;
        
        public float Speed => speed + _modif;
        public BoolReactiveProperty IsCanMoveHorizontal { get; } = new BoolReactiveProperty(true);
    }
}