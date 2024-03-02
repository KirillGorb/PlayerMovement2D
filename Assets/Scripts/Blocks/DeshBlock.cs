using System.Collections;
using Play.Input;
using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class DeshBlock : MonoBehaviour
    {
        [SerializeField] private float timeAiming;
        [SerializeField] private float timeFly;
        [SerializeField] private float force;

        private Vector2 _directionMove;
        private ISettingMoveble _setting;

        private bool _isDesh = false;
        private bool _isTimeOut = true;

        [Inject]
        private void Injecting(InputCenter inputCenter)
        {
            inputCenter.DeshInput.Subscribe(e =>
            {
                if (e != Vector2.zero && !_isTimeOut)
                {
                    _isDesh = true;
                    _directionMove = e;
                }
            });
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting) && _isTimeOut)
            {
                _setting = setting;
                _isDesh = false;
                StartCoroutine(TimerAiming());
            }
        }

        private IEnumerator TimerAiming()
        {
            _setting.MoveSetting.Activator.OnDisactiveMove(this);
            _setting.JumpSettings.Activator.OnDisactiveMove(this);
            _setting.GetRigidbody2D.velocity = Vector2.zero;

            _isTimeOut = false;
            yield return new WaitForSeconds(timeAiming);
            _isTimeOut = true;

            if (!_isDesh)
            {
                _setting.MoveSetting.Activator.OnActiveMove(this);
                _setting.JumpSettings.Activator.OnActiveMove(this);
            }
            else
            {
                _setting.GetRigidbody2D.velocity = _directionMove.normalized * force;
                StartCoroutine(TimerFly());
            }
        }

        private IEnumerator TimerFly()
        {
            yield return new WaitForSeconds(timeFly);
            _setting.MoveSetting.Activator.OnActiveMove(this);
            _setting.JumpSettings.Activator.OnActiveMove(this);
        }
    }
}