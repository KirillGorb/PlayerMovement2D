using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public class RotateIn3DBlock : MonoBehaviour
    {
        [SerializeField] private Transform pointConnect;
        [SerializeField] private float speedRot;

        private ISettingMoveble _setting;
        private bool _isConnect = false;

        private async void Update()
        {
            if (!_isConnect) return;

            transform.Rotate(0, speedRot, 0);
            _setting.GetTransform.position = new(pointConnect.position.x, _setting.GetTransform.position.y, pointConnect.position.z);
            if (_setting.GetTransform.position.z >= 0)
            {
                _isConnect = false;
                _setting.JumpSettings.IsCanJump.Value = true;
                _setting.IsCanMoveHorizontal.Value = true;
                await MovebleTo.MoveToAsync(_setting.GetTransform, new(_setting.GetTransform.position.x, _setting.GetTransform.position.y, 0), 5);
                Destroy(this);
            }
        }

        private async void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ISettingMoveble setting))
            {
                _setting = setting;
                setting.JumpSettings.IsCanJump.Value = false;
                setting.IsCanMoveHorizontal.Value = false;
                await MovebleTo.MoveToAsync(_setting.GetTransform, new(pointConnect.position.x, _setting.GetTransform.position.y,pointConnect.position.z), 5);
                _isConnect = true;
            }
        }
    }
}