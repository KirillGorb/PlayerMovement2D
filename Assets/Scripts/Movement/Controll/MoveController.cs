using Play.Input;
using Play.Movement.Abstraction;
using Play.Movement.Setting;
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

        [SerializeField] private JumpSetting jumpSetting;
        [SerializeField] private MovementHorizontalSetting horizontalSetting;

        [SerializeField] private float higthPlayer;

        private CheckInput _input;
        private MoveHorizontal _moveHorizontal;
        private JumpLogic _jump;

        public MovementHorizontalSetting MoveSetting => horizontalSetting;
        public JumpSetting JumpSettings => jumpSetting;
        public Rigidbody2D GetRigidbody2D => _rd;
        public Transform GetTransform => transform;

        public bool IsValueUpPlayer(Transform posTarget) =>
            //Physics2D.Raycast(transform.position, Vector2.down, higthPlayer).collider.gameObject == posTarget;
            posTarget.position.y + higthPlayer <= transform.position.y;

        [Inject]
        private void Inject(CheckInput input)
        {
            _input = input;
        }

        private void Start()
        {
            _moveHorizontal = new MoveHorizontal();
            _moveHorizontal.Init(_rd);
            _moveHorizontal.Init(_input);
            _moveHorizontal.Init(horizontalSetting);

            _jump = new JumpLogic();
            _jump.Init(_rd);
            _jump.Init(_input);
            _jump.Init(jumpSetting);
            _jump.Init((_checkGroundDown, _checkGroundHead));
        }
    }
}