using System.Collections;
using Play.Input;
using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class Adhesive : MonoBehaviour
    {
        [SerializeField] private DirectionMovement moveDir;
        [SerializeField] private DirectionMovement jumpDir;
        [SerializeField] private float speed;
        [SerializeField] private float impulceJump;
        [SerializeField] private float timeFly;

        private ISettingMoveble _setting;
        private Vector2 _dir;
        private bool _isConnect;

        private void Start()
        {
            _dir = moveDir.CheckDirectionWind();
        }

        [Inject]
        private void Injecting(InputCenter inputCenter)
        {
            inputCenter.DeshInput.Subscribe(e =>
            {
                if (_isConnect)
                    _setting.GetRigidbody2D.velocity = e * _dir * speed;
            });
            inputCenter.JumpInput.Subscribe(e =>
            {
                if (e && _isConnect) StartCoroutine(TimerJump());
            });
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting) && !_isConnect)
            {
                _setting = setting;
                Desconect(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting) && _isConnect)
                Desconect(false);
        }

        private IEnumerator TimerJump()
        {
            _isConnect = false;
            var dirJ = jumpDir.CheckDirectionWind();
            if (dirJ == Vector2.down || dirJ == Vector2.up || dirJ == Vector2.zero)
            {
                _setting.MoveSetting.Activator.OnActiveMove(this);
                _setting.GetRigidbody2D.velocity = dirJ * impulceJump;
            }
            else
                _setting.GetRigidbody2D.velocity = (dirJ + _dir) * impulceJump;

            yield return new WaitForSeconds(timeFly);
            _setting.MoveSetting.Activator.OnActiveMove(this);
            _setting.JumpSettings.Activator.OnActiveMove(this);
        }

        private void Desconect(bool isActive)
        {
            _isConnect = isActive;
            if (isActive)
            {
                _setting.MoveSetting.Activator.OnDisactiveMove(this);
                _setting.JumpSettings.Activator.OnDisactiveMove(this);
            }
            else
            {
                _setting.MoveSetting.Activator.OnActiveMove(this);
                _setting.JumpSettings.Activator.OnActiveMove(this);
            }
        }
    }
}