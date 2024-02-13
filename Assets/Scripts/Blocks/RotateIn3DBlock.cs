using Play.Movement.Abstraction;
using UnityEngine;
using System.Collections;

namespace Play.Block
{
    public class RotateIn3DBlock : MonoBehaviour
    {
        [SerializeField] private Transform pointConnect;
        [SerializeField] private float speedRot;

        private Vector3 _offset;
        private ISettingMoveble _setting;
        private bool _isConnect = false;

        private void Update()
        {
            if (!_isConnect) return;

            transform.Rotate(0, speedRot, 0);
            _setting.GetTransform.position = pointConnect.position + _offset;
            if (_setting.GetTransform.position.z >= 0)
            {
                _isConnect = false;
                _setting.JumpSettings.IsCanJump.Value = true;
                _setting.IsCanMoveHorizontal.Value = true;
                _setting.GetTransform.position = new Vector3(_setting.GetTransform.position.x, _setting.GetTransform.position.y, 0);
                Destroy(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ISettingMoveble setting))
            {
                _setting = setting;
                setting.JumpSettings.IsCanJump.Value = false;
                setting.IsCanMoveHorizontal.Value = false;
                _isConnect = true;
                _offset = collision.transform.position - pointConnect.position;
            }
        }
    }
}