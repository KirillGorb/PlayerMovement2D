using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Play.Input
{
    public class InputCenter : MonoBehaviour
    {
        private CompositeDisposable _disposables = new();
        private PlayerInput _input;
        private bool _clickJumpValue;

        public ObserverFor<Vector2> DeshInput { get; } = new();
        public ObserverFor<float> HorizontalInput { get; } = new();
        public ObserverFor<bool> JumpInput { get; } = new();
        public BoolReactiveProperty SharpDescentInput { get; } = new();

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
            _input.Movement.SharpDescent.canceled += e => SharpDescentInput.Value = false;
            _input.Movement.SharpDescent.performed += e => SharpDescentInput.Value = true;

            JumpInput.SetValue(Observable.EveryFixedUpdate(),
                () => _clickJumpValue).AddTo(_disposables);

            HorizontalInput.SetValue(Observable.EveryFixedUpdate(),
                () => _input.Movement.Horizontal.ReadValue<float>()).AddTo(_disposables);

            DeshInput.SetValue(Observable.EveryFixedUpdate(),
                () => _input.Movement.Desh.ReadValue<Vector2>()).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _input.Disable();
            _disposables.Dispose();
        }
    }
}