using Play.Input;
using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class MoveGropUpdater : MonoBehaviour
    {
        private Vector3 _offset;
        private Transform _target;
        private bool _isMove = false;

        public void SetData(Vector2 offset, Transform target, bool isMove)
        {
            _offset = offset;
            _target = target;
            _isMove = isMove;
        }

        [Inject]
        private void Inject(CheckInput input, ISettingMoveble setting)
        {
            input.HorizontalInput.Subscribe(e =>
            {
                if (_isMove)
                {
                    _offset += e * setting.MoveSetting.Speed * Vector3.right * Time.deltaTime;
                    setting.GetTransform.position = _target.position + _offset;
                }
            });
            input.JumpInput.Subscribe(e =>
            {
                if (e && _isMove) _isMove = false;
            });
        }
    }
}