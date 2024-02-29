using System;

namespace UniRx
{
    public class ObserverFor<T> : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private Action<T> _change;
        private IObservable<long> _everyChange;

        public IDisposable SetValue(IObservable<long> everyChange, Func<T> value)
        {
            _everyChange = everyChange;
            _everyChange.Subscribe(_ => _change(value())).AddTo(_disposables);
            return this;
        }

        public void Subscribe(Action<T> addAction) => _change += addAction;
        public void Desubscribe(Action<T> addAction) => _change -= addAction;

        public void Dispose() => _disposables.Clear();
    }
}