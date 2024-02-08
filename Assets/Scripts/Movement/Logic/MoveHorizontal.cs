using Play.Abstraction;
using Play.Input;
using Play.Movement.Setting;
using System;
using UnityEngine;

namespace Play.Movement.Controller
{
    public class MoveHorizontal : IInit<CheckInput>, IInit<MovementHorizontallSetting>, IInit<Rigidbody2D>
    {
        private MovementHorizontallSetting _setting;
        private Rigidbody2D _rd;

        #region Init
        public void Init(CheckInput data) => data.HorizontalInput.Subscribe(Move);
        public void Init(MovementHorizontallSetting data) => _setting = data;
        public void Init(Rigidbody2D data) => _rd = data;
        #endregion

        private void Move(float directionInput)
        {
            if (_setting.IsCanMoveHorizontal.Value)
                _rd.velocity = new Vector2(_setting.Speed * directionInput, _rd.velocity.y);
        }
    }
}