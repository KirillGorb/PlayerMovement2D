using System;

namespace UniRx
{
    public class ObserverForUpdate<T> : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private Func<T> value;

        public IDisposable SetValue(Func<T> func)
        {
            value = func;
            return this;
        }

        public void Subscribe(Action<T> addAction) => Observable.EveryUpdate().Subscribe(_ => addAction(value())).AddTo(_disposables);

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}