using Play.Input;
using Play.Movement.Abstraction;
using Play.Movement.Setting;
using UniRx;
using UnityEngine;
using Zenject;

namespace Play.Movement.Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveController : MonoBehaviour, ISettingMoveble
    {
        [SerializeField] private Rigidbody2D _rd;
        [SerializeField] private CheckGroundController _checkGroundDown;
        [SerializeField] private CheckGroundController _checkGroundHead;

        private MovementHorizontalSetting _horizontalSetting;
        private JumpSetting _jumpSetting;
        private CheckInput _input;
        private MoveHorizontal _moveHorizontal;
        private JumpLogic _jump;

        public MovementHorizontalSetting MoveSetting => _horizontalSetting;
        public JumpSetting JumpSettings => _jumpSetting;
        public Rigidbody2D GetRigidbody2D => _rd;
        public Transform GetTransform => transform;

        [Inject]
        private void Init(MovementHorizontalSetting horizontalSetting, JumpSetting jumpSetting, CheckInput input)
        {
            _horizontalSetting = horizontalSetting;
            _jumpSetting = jumpSetting;
            _input = input;
        }

        private void Start()
        {
            _moveHorizontal = new MoveHorizontal();
            _moveHorizontal.Init(_rd);
            _moveHorizontal.Init(_input);
            _moveHorizontal.Init(_horizontalSetting);

            _jump = new JumpLogic();
            _jump.Init(_rd);
            _jump.Init(_input);
            _jump.Init(_jumpSetting);
            _jump.Init((_checkGroundDown, _checkGroundHead));
        }
    }
}