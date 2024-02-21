using UniRx;
using UnityEngine;

namespace Play.Movement.Setting
{
    [CreateAssetMenu()]
    public class MovementHorizontalSetting : ScriptableObject
    {
        [SerializeField] private float speed;

        public float Speed => speed;
        public BoolReactiveProperty IsCanMoveHorizontal { get; } = new BoolReactiveProperty(true);
    }
}