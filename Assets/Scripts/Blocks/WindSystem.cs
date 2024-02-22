using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public enum WindDirection
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3,
        None
    }

    public class WindSystem : MonoBehaviour
    {
        [SerializeField] private WindDirection directionType;
        [SerializeField] private float forceWind;

        private Vector2 _directionMove;
        private float _directionX;
        private float _directionY;

        private void Start()
        {
            CheckDirectionWind();
        }

        private void CheckDirectionWind()
        {
            switch (directionType)
            {
                case WindDirection.Up:
                    _directionX = 0;
                    _directionY = 1;
                    _directionMove = Vector2.up;
                    break;
                case WindDirection.Down:
                    _directionX = 0;
                    _directionY = -1;
                    _directionMove = Vector2.down;
                    break;
                case WindDirection.Right:
                    _directionX = -1;
                    _directionY = 0;
                    _directionMove = Vector2.right;
                    break;
                case WindDirection.Left:
                    _directionX = 1;
                    _directionY = 0;
                    _directionMove = Vector2.left;
                    break;
                case WindDirection.None:
                    _directionX = 0;
                    _directionY = 0;
                    _directionMove = Vector2.zero;
                    break;
            }
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