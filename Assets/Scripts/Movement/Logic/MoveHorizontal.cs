using System;
using Play.Abstraction;
using Play.Input;
using Play.Movement.Setting;
using UnityEngine;

namespace Play.Movement.Controller
{
    public class MoveHorizontal : IInit<InputCenter>, IInit<MovementHorizontalSetting>, IInit<Rigidbody2D>
    {
        private MovementHorizontalSetting _setting;
        private Rigidbody2D _rd;

        private float _value;

        #region Init

        public void Init(InputCenter data) => data.HorizontalInput.Subscribe(Move);

        public void Init(MovementHorizontalSetting data)
        {
            _setting = data;
            _setting.Activator.Subscribe(e => _value = 0);
        }

        public void Init(Rigidbody2D data) => _rd = data;

        #endregion

        private void Move(float directionInput)
        {
            if (!_setting.Activator.GetCan) return;

            _rd.velocity = new Vector2(_setting.Speed * DirSlow(directionInput) + _setting.ModValueX.GetMod,
                _rd.velocity.y);
        }

        private float DirSlow(float dir)
        {
            if (dir != 0)
            {
                _value += _setting.Slow * Time.deltaTime * dir;
                _value = Math.Clamp(_value, -1, 1);
                return _value;
            }

            return _value = 0;
        }
    }
}