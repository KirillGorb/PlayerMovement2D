using UniRx;
using UnityEngine;

namespace Play.Input
{
    public class CheckInput : MonoBehaviour
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        private PlayerInput _input;
        private bool _clickJumpValue;

        public ObserverForUpdate<float> HorizontalInput { get; } = new ObserverForUpdate<float>();
        public ObserverForUpdate<bool> JumpInput { get; } = new ObserverForUpdate<bool>();
        public BoolReactiveProperty SharpDescent { get; } = new BoolReactiveProperty();

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void Start()
        {
            _input.Movement.Jump.performed += e => _clickJumpValue = true;
            _input.Movement.Jump.canceled += e => _clickJumpValue = false;

            JumpInput.SetValue(() => _clickJumpValue).AddTo(_disposables);
            HorizontalInput.SetValue(() => _input.Movement.Horizontal.ReadValue<float>()).AddTo(_disposables);

            _input.Movement.SharpDescent.canceled += e => SharpDescent.Value = false;
            _input.Movement.SharpDescent.performed += e => SharpDescent.Value = true;
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}