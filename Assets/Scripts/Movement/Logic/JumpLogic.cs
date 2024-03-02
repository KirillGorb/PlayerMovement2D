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

        private float _jumpImpulse;
        private float _dopJumpHightTime;
        private float _gravityValue;
        private float _timePoolClick;
        private int _countDopJump;

        private bool _isJumpFly;
        private bool _isPoolJumpClick;
        private bool _isClickDown;
        private bool _dirJumpClick;

        private CheckGroundController _checkGround;
        private CheckGroundController _checkHead;
        private JumpSetting _setting;
        private Rigidbody2D _rd;

        #region Init

        public void Init(InputCenter data)
        {
            data.JumpInput.Subscribe(Jump);
            data.SharpDescentInput.Subscribe(SharpDescent).AddTo(_disposables);
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

            if (_checkGround.CheckGround) _countDopJump = _setting.CountDopJump;
            
            PoolClick(jumpClick);
            DopHightJump(jumpClick);
            DopJump(jumpClick);

            if (_isJumpFly)
            {
                if (_jumpImpulse >= 0 && !_checkHead.CheckGround)
                    _jumpImpulse -= Time.deltaTime * _setting.Velocity;
                else
                    _isJumpFly = false;

                _rd.velocity = new Vector2(_rd.velocity.x, _jumpImpulse);
                return;
            }

            if (!_isJumpFly && _checkGround.CheckGround && (jumpClick || _isPoolJumpClick))
                DataJump();
        }

        private void DataJump()
        {
            _countDopJump--;
            _jumpImpulse = _setting.JumpImpulse;
            _dopJumpHightTime = _setting.DopHight;
            _isJumpFly = true;
        }

        private void DopHightJump(bool jumpClick)
        {
            if (jumpClick && _dopJumpHightTime >= 0)
            {
                _dopJumpHightTime -= Time.deltaTime;
                _jumpImpulse = _setting.JumpImpulse;
            }
        }

        private void DopJump(bool jumpClick)
        {
            if (jumpClick && _dirJumpClick)
                _isClickDown = true;
            _dirJumpClick = !jumpClick;

            if (_isClickDown && _countDopJump > 0)
                DataJump();
            _isClickDown = false;
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

        private void SharpDescent(bool isClick)
        {
            if (!_setting.Activator.GetCan) return;
            if (isClick && !_checkGround.CheckGround)
            {
                _isJumpFly = false;
                _rd.AddForce(-_setting.GravityDownMove * Vector2.up);
            }
        }

        public void Dispose()
        {
            _setting.Activator.Dispose();
            _disposables.Clear();
        }
    }
}