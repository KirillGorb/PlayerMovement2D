using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public class WindSystem : MonoBehaviour
    {
        [SerializeField] private DirectionMovement dir;
        [SerializeField] private float forceWind;

        private Vector2 _directionMove;
        private float _directionX;
        private float _directionY;

        private void Start()
        {
            _directionMove = dir.CheckDirectionWind();
            _directionX = _directionMove.x;
            _directionY = _directionMove.y;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Rigidbody2D rd) && !other.TryGetComponent(out ISettingMoveble setting))
            {
                rd.velocity /= _directionMove * forceWind;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting))
            {
                setting.MoveSetting.ModValueX.AddModifSpeed(forceWind * _directionX);
                setting.JumpSettings.ModValueY.AddModifSpeed(forceWind * _directionY);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting))
            {
                setting.MoveSetting.ModValueX.RemoveModifSpeed(forceWind * _directionX);
                setting.JumpSettings.ModValueY.RemoveModifSpeed(forceWind * _directionY);
            }
        }
    }
}