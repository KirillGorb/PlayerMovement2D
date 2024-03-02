using System;
using Play.Input;
using Play.Movement.Abstraction;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class MoveGropUpdater : MonoBehaviour
    {
        private Vector3 _offset;
        private float _dirInput;
        private Transform _target;
        private ISettingMoveble _setting;
        private bool _isMove;

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
            inputCenter.HorizontalInput.Subscribe(e => _dirInput = e);
            inputCenter.JumpInput.Subscribe(e =>
            {
                if (e && _isMove) _isMove = false;
            });
        }

        private void Update()
        {
            if (_isMove)
            {
                _offset += Vector3.right * _dirInput * _setting.MoveSetting.Speed * Time.deltaTime;
                _setting.GetTransform.position = _target.position + _offset;
            }
        }
    }
}