using Play.Input;
using Play.Movement.Setting;
using Play.Movement.Abstraction;
using UnityEngine;
using UniRx;

namespace Play.Movement.Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveController : MonoBehaviour, ISettingMoveble
    {
        [SerializeField] private MovementHorizontallSetting _horizontallSetting;
        [SerializeField] private JumpSetting _jumpSetting;
        [SerializeField] private CheckInput _input;
        [SerializeField] private Rigidbody2D _rd;
        [SerializeField] private CheckGroundController _checkGroundDown;
        [SerializeField] private CheckGroundController _checkGroundHead;

        private MoveHorizontal _moveHorizontal;
        private JumpLogic _jump;

        public BoolReactiveProperty IsCanMoveHorizontal => _horizontallSetting.IsCanMoveHorizontal;
        public JumpSetting JumpSettings => _jumpSetting;
        public Rigidbody2D GetRigidbody2D => _rd;
        public Transform GetTransform => transform;

        private void Start()
        {
            _moveHorizontal = new MoveHorizontal();
            _moveHorizontal.Init(_rd);
            _moveHorizontal.Init(_horizontallSetting);
            _moveHorizontal.Init(_input);

            _jump = new JumpLogic();
            _jump.Init(_rd);
            _jump.Init(_jumpSetting);
            _jump.Init((_checkGroundDown, _checkGroundHead));
            _jump.Init(_input);
        }

        private void OnDisable()
        {
            _jump.Dispose();
        }
    }
}