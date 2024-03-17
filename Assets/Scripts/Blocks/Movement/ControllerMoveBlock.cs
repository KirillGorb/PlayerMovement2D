using System;
using Play.Input;
using Play.Movement.Abstraction;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class ControllerMoveBlock : MonoBehaviour
    {
        [SerializeField] private float speed;

        private bool _isMove = false;
        private Vector3 _offset;
        private Vector3 _dir;
        private ISettingMoveble _setting;

        [Inject]
        private void Injecting(InputCenter inputCenter)
        {
            inputCenter.DeshInput.Subscribe(e => _dir = e);
            inputCenter.JumpInput.Subscribe(e =>
            {
                if (e && _isMove)
                    Disconect();
            });
        }

        private void Update()
        {
            if (_isMove)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, transform.position + _dir,
                        speed * Time.deltaTime);
                _setting.GetTransform.position = transform.position + _offset;
            }
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