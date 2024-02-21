using System;
using Play.Abstraction;
using Play.Input;
using Play.Movement.Setting;
using UniRx;
using UnityEngine;

namespace Play.Movement.Controller
{
    public class JumpLogic : IDisposable, IInit<CheckInput>, IInit<JumpSetting>, IInit<Rigidbody2D>,
        IInit<(CheckGroundController, CheckGroundController)>
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private float _startJumpImpulse;
        private float _jumpTime;
        private float _jumpImpulse;
        private float _gravity;
        private float _dopJumpHightTime;

        private bool _isJumpFly = false;
        private bool _sharpDescent = false;

        private CheckGroundController _checkGround;
        private CheckGroundController _checkHead;
        private JumpSetting _setting;
        private Rigidbody2D _rd;

        #region Init

        public void Init(CheckInput data)
        {
            data.JumpInput.Subscribe(Jump);
            data.SharpDescent.Subscribe(e => _sharpDescent = e).AddTo(_disposables);
        }

        public void Init(JumpSetting data) => _setting = data;
        public void Init(Rigidbody2D data) => _rd = data;

        public void Init((CheckGroundController, CheckGroundController) checkGroundHead)
        {
            _checkGround = checkGroundHead.Item1;
            _checkHead = checkGroundHead.Item2;
        }

        #endregion

        private void Jump(bool jumpClick)
        {
            if (!_setting.IsCanJump.Value) return;

            if (_isJumpFly)
            {
                if (jumpClick && _dopJumpHightTime >= 0)
                {
                    _dopJumpHightTime -= Time.deltaTime;
                    _jumpImpulse = _setting.JumpImpulse;
                    _jumpTime = _setting.JumpTime;
                }

                if ((_jumpTime > 0 || _jumpImpulse > 0) && !_checkHead.CheckGround && !_sharpDescent)
                {
                    _jumpImpulse -= _setting.Velocity * Time.deltaTime;
                    _jumpTime -= Time.deltaTime;
                    SetJump(Mathf.Max(_jumpImpulse, 0));
                }
                else
                {
                    _jumpTime = _setting.JumpTime;
                    _jumpImpulse = _startJumpImpulse;
                    _gravity = _sharpDescent ? _setting.GravityDownMove : 0;
                    _isJumpFly = false;
                    return;
                }

                return;
            }

            FlyDown();
            if (_checkGround.CheckGround && jumpClick)
            {
                _isJumpFly = true;
                _dopJumpHightTime = _setting.DopTimeFly;
                _jumpImpulse = _setting.JumpImpulse;
                _jumpTime = _setting.JumpTime;
            }
        }

        private void FlyDown()
        {
            if (!_checkGround.CheckGround)
            {
                if (_sharpDescent) _gravity = _setting.GravityDownMove;
                _gravity += _setting.VelocityGravity * Time.deltaTime;
                SetJump(-Mathf.Min(_gravity, _setting.MaxGravity));
            }
            else
            {
                SetJump(Mathf.Min(0, _rd.velocity.y));
                _gravity = 0;
            }
        }

        private void SetJump(float y) =>
            _rd.velocity = new Vector2(_rd.velocity.x, y);

        public void Dispose() => _disposables.Clear();
    }
}