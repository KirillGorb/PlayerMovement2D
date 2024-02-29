using System;
using Play.Abstraction;
using Play.Input;
using Play.Movement.Setting;
using UniRx;
using UnityEngine;

namespace Play.Movement.Controller
{
    public class JumpLogic : IDisposable, IInit<InputCenter>, IInit<JumpSetting>, IInit<Rigidbody2D>,
        IInit<(CheckGroundController, CheckGroundController)>
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private float _startJumpImpulse;
        private float _jumpTime;
        private float _jumpImpulse;
        private float _dopJumpHightTime;
        private float _gravityValue;
        private float _timePoolClick;

        private bool _isJumpFly;
        private bool _sharpDescent;
        private bool _isPoolJumpClick;

        private CheckGroundController _checkGround;
        private CheckGroundController _checkHead;
        private JumpSetting _setting;
        private Rigidbody2D _rd;

        #region Init

        public void Init(InputCenter data)
        {
            data.JumpInput.Subscribe(Jump);
            data.SharpDescentInput.Subscribe(e => _sharpDescent = e).AddTo(_disposables);
        }

        public void Init(JumpSetting data)
        {
            _setting = data;
            _setting.Activator.Subscribe(e => _rd.gravityScale = e ? _gravityValue : 0);
        }

        public void Init(Rigidbody2D data)
        {
            _rd = data;
            _gravityValue = _rd.gravityScale;
        }

        public void Init((CheckGroundController, CheckGroundController) checkGroundHead)
        {
            _checkGround = checkGroundHead.Item1;
            _checkHead = checkGroundHead.Item2;
        }

        #endregion

        private void Jump(bool jumpClick)
        {
            if (!_setting.Activator.GetCan) return;

            PoolClick(jumpClick);

            if (_sharpDescent && !_checkGround.CheckGround)
                SetJump(-_setting.GravityDownMove);

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
                    _isJumpFly = false;
                    return;
                }

                return;
            }

            if (_checkGround.CheckGround && (jumpClick || _isPoolJumpClick))
            {
                _isJumpFly = true;
                _isPoolJumpClick = false;
                _dopJumpHightTime = _setting.DopTimeFly;
                _jumpImpulse = _setting.JumpImpulse;
                _jumpTime = _setting.JumpTime;
            }
        }

        private void PoolClick(bool isClick)
        {
            if (isClick)
            {
                _timePoolClick = _setting.PoolTimeClick;
                _isPoolJumpClick = true;
            }

            if (_timePoolClick >= 0 && _isPoolJumpClick)
                _timePoolClick -= Time.deltaTime;
            else
                _isPoolJumpClick = false;
        }

        private void SetJump(float y) =>
            _rd.velocity = new Vector2(_rd.velocity.x, y + _setting.ModValueY.GetMod);

        public void Dispose()
        {
            _setting.Activator.Dispose();
            _disposables.Clear();
        }
    }
}