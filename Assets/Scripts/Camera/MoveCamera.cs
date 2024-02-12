using UnityEngine;
using UniRx;

namespace Play.Camera
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float speed;

        private CompositeDisposable _disposables = new CompositeDisposable();

        private void Start() =>
            Observable.EveryLateUpdate().Subscribe(_ => Move()).AddTo(_disposables);

        private void OnDestroy() =>
            _disposables.Clear();

        private void Move() =>
            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
    }
}