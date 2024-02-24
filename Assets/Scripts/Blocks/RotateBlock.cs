using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class RotateBlock : MonoBehaviour, IUpdater
    {
        [SerializeField] private float speed;

        [Inject]
        private void Init(Rotate3DUpdater updater) => updater.AddCheck(this);

        public void Update() => transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}