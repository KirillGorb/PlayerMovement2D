using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class RotateIn3DBlock : MonoBehaviour, IUpdater
    {
        [SerializeField] private Transform pointConnect;
        [SerializeField] private float speedRot;

        private ISettingMoveble _setting;
        private Rotate3DUpdater _updater;

        private bool _isConnect;

        [Inject]
        private void Init(Rotate3DUpdater updater) => _updater = updater;

        public void Update()
        {
            if (!_isConnect) return;

            transform.Rotate(0, speedRot, 0);
            _setting.GetTransform.position = new(pointConnect.position.x, _setting.GetTransform.position.y,
                pointConnect.position.z);
            if (_setting.GetTransform.position.z >= 0)
            {
                _isConnect = false;
                _setting.JumpSettings.Activator.OnActiveMove(this);
                _setting.MoveSetting.Activator.OnActiveMove(this);
                MovebleTo.MoveToAsync(_setting.GetTransform,
                    new(_setting.GetTransform.position.x, _setting.GetTransform.position.y, 0), 5,
                    0.2f, () => _updater.RemoveCheck(this));
            }
        }

        private async void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ISettingMoveble setting) &&
                setting.IsValueUpPlayer(transform))
            {
                _setting = setting;
                _setting.JumpSettings.Activator.OnDisactiveMove(this);
                _setting.MoveSetting.Activator.OnDisactiveMove(this);
                await MovebleTo.MoveToAsync(_setting.GetTransform,
                    new(pointConnect.position.x, _setting.GetTransform.position.y, pointConnect.position.z), 5, 0.2f);
                _isConnect = true;
                _updater.AddCheck(this);
            }
        }
    }
}