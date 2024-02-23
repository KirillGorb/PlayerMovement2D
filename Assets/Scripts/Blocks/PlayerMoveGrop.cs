using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class PlayerMoveGrop : MonoBehaviour
    {
        private Vector2 offset;
        private ISettingMoveble _setting;

        [Inject] private MoveGropUpdater _mover;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ISettingMoveble setting) && setting.IsValueUpPlayer(transform))
            {
                _setting = setting;
                _setting.MoveSetting.IsCanMoveHorizontal.Value = false;
                offset = setting.GetTransform.position - transform.position;
                _mover.SetData(offset, transform, true);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ISettingMoveble setting))
            {
                setting.MoveSetting.IsCanMoveHorizontal.Value = true;
                _mover.SetData(offset = Vector2.zero, transform, false);
            }
        }
    }
}