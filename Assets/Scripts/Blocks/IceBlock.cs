using System;
using System.Collections;
using Play.Input;
using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class IceBlock : MonoBehaviour
    {
        [SerializeField] private float velocitySpeed;
        [SerializeField] private float timeFree;

        private bool _isConnect;
        private float _dirMove;
        private ISettingMoveble _setting;

        [Inject]
        private void Injecting(InputCenter inputCenter)
        {
            inputCenter.JumpInput.Subscribe(e =>
            {
                if (e && _isConnect)
                    StartCoroutine(TimerActive());
            });
            inputCenter.HorizontalInput.Subscribe(e => _dirMove = e);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out ISettingMoveble setting) &&
                other.gameObject.TryGetComponent(out Rigidbody2D rd))
                rd.velocity *= velocitySpeed;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!_isConnect && _dirMove != 0 && other.gameObject.TryGetComponent(out ISettingMoveble setting) &&
                setting.IsValueUpPlayer(transform))
            {
                _isConnect = true;
                _setting = setting;
                _setting.MoveSetting.Activator.OnDisactiveMove(this);
                _setting.GetRigidbody2D.velocity = new(_dirMove * _setting.MoveSetting.Speed * velocitySpeed,
                    _setting.GetRigidbody2D.velocity.y);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (_isConnect && other.gameObject.TryGetComponent(out ISettingMoveble setting))
                StartCoroutine(TimerActive());
        }

        private IEnumerator TimerActive()
        {
            _isConnect = false;
            yield return new WaitForSeconds(timeFree);
            _setting.MoveSetting.Activator.OnActiveMove(this);
        }
    }
}