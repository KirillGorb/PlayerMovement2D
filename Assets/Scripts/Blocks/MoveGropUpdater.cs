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
        private ISettingMoveble _setting;
        private bool _isMove = false;

        public void SetData(Vector2 offset, Transform target, bool isMove)
        {
            _offset = offset;
            _target = target;
            _isMove = isMove;
            if (isMove)
                _setting.MoveSetting.Activator.OnDisactiveMove(this);
            else
                _setting.MoveSetting.Activator.OnActiveMove(this);
        }

        [Inject]
        private void Inject(InputCenter inputCenter, ISettingMoveble setting)
        {
            _setting = setting;
            inputCenter.HorizontalInput.Subscribe(e =>
            {
                if (_isMove)
                {
                    _offset += e * _setting.MoveSetting.Speed * Vector3.right * Time.deltaTime;
                    _setting.GetTransform.position = _target.position + _offset;
                }
            });
            inputCenter.JumpInput.Subscribe(e =>
            {
                if (e && _isMove) _isMove = false;
            });
        }
    }
}