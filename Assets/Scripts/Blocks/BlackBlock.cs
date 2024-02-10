using Play.Movement.Abstraction;
using Play.Input;
using UnityEngine;
using System.Collections;

namespace Play.Block
{
    public class BlackBlock : MonoBehaviour
    {
        [SerializeField] private CheckInput input;

        private Rigidbody2D _playerRD;
        private Transform _playerPos;
        private ISettingMoveble _setting;
        private bool _isConect = false;
        private static bool IsConectPLayer;

        private void Start()
        {
            IsConectPLayer = false;
            input.JumpInput.Subscribe(e => { if (e && _isConect) StartCoroutine(TimerDisconnect()); });
        }

        private void Update()
        {
            if (!_isConect) return;
            Vector2 dir = transform.position - _playerPos.position;
            _playerRD.velocity += dir / 4;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ISettingMoveble setting) && !IsConectPLayer)
            {
                _setting = setting;
                _setting.JumpSettings.IsCanJump.Value = false;
                _setting.IsCanMoveHorizontal.Value = false;
                _playerRD = _setting.GetRigidbody2D;
                _playerPos = _setting.GetTransform;

                IsConectPLayer = true;
                _isConect = true;
            }
        }

        private IEnumerator TimerDisconnect()
        {
            _isConect = false;
            yield return new WaitForSeconds(_playerRD.velocity.magnitude / (_setting.JumpSettings.GravityDownMove * 2.5f));
            _playerRD.velocity = Vector2.down * _setting.JumpSettings.GravityDownMove / 4;
            _setting.JumpSettings.IsCanJump.Value = true;
            _setting.IsCanMoveHorizontal.Value = true;
            IsConectPLayer = false;
        }
    }
}