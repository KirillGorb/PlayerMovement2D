using UniRx;
using Play.Movement.Setting;
using UnityEngine;

namespace Play.Movement.Abstraction
{
    public interface ISettingMoveble
    {
        public MovementHorizontalSetting MoveSetting { get; }

        public JumpSetting JumpSettings { get; }

        public Rigidbody2D GetRigidbody2D { get; }

        public Transform GetTransform { get; }
    }
}