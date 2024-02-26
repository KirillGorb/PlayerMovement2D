using Play.Input;
using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class ControllerMoveBlock : MonoBehaviour
    {
        [SerializeField] private float speed;

        private bool _isMove = false;
        private Vector3 _offset;
        private ISettingMoveble _setting;

        [Inject]
        private void Injecting(CheckInput input)
        {
            input.DeshInput.Subscribe(e =>
            {
                if (_isMove)
                {
                    transform.position =
                        Vector2.MoveTowards(transform.position, transform.position + (Vector3)e,
                            speed * Time.deltaTime);
                    _setting.GetTransform.position = transform.position + _offset;
                }
            });
            input.JumpInput.Subscribe(e =>
            {
                if (e && _isMove)
                    Disconect();
            });
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ISettingMoveble setting) && setting.IsValueUpPlayer(transform))
            {
                _setting = setting;
                _isMove = true;
                setting.MoveSetting.Activator.OnDisactiveMove(this);
                _offset = setting.GetTransform.position - transform.position;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ISettingMoveble setting) && _isMove)
                Disconect();
        }

        private void Disconect()
        {
            _isMove = false;
            _setting.MoveSetting.Activator.OnActiveMove(this);
        }
    }
}