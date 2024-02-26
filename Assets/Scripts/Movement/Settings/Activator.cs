using System;
using System.Collections.Generic;
using UniRx;
using Object = UnityEngine.Object;

namespace Play.Movement.Setting
{
    public class Activator : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private List<Object> _activators = new List<Object>();
        private BoolReactiveProperty _isCanAction;

        public bool GetCan => _isCanAction.Value;

        public Activator(BoolReactiveProperty isCanAction) => _isCanAction = isCanAction;

        public void Subscribe(Action<bool> action) => _isCanAction.Subscribe(action).AddTo(_disposables);

        public void OnDisactiveMove(Object other)
        {
            if (!_activators.Contains(other))
                _activators.Add(other);
            _isCanAction.Value = false;
        }

        public void OnActiveMove(Object other)
        {
            _activators.Remove(other);
            if (_activators.Count <= 0)
                _isCanAction.Value = true;
        }

        public void Dispose()
        {
            _isCanAction?.Dispose();
            _disposables?.Dispose();
        }
    }
}