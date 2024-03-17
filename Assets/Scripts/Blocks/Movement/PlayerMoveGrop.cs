using Play.Movement.Abstraction;
using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class PlayerMoveGrop : MonoBehaviour
    {
        [Inject] private MoveGropUpdater _mover;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ISettingMoveble setting) && setting.IsValueUpPlayer(transform))
                _mover.SetData(setting.GetTransform.position - transform.position, transform, true);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ISettingMoveble setting))
                _mover.SetData(Vector2.zero, transform, false);
        }
    }
}