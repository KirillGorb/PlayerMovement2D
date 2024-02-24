using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public class CobwebBlock : MonoBehaviour
    {
        [SerializeField] private float speedDown;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting))
            {
                setting.MoveSetting.ModValueSpeed.AddModifSpeed(speedDown);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting))
            {
                setting.MoveSetting.ModValueSpeed.RemoveModifSpeed(speedDown);
            }
        }
    }
}