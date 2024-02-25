using System.Collections;
using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public class CobwebBlock : MonoBehaviour
    {
        [SerializeField] private float speedDown;
        [SerializeField] private float timeSlow;

        [SerializeField] private bool isSlowTime;

        private ISettingMoveble _setting;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting))
            {
                _setting = setting;
                _setting.MoveSetting.ModValueSpeed.AddModifSpeed(speedDown);
                if (isSlowTime)
                    StartCoroutine(TimerSlowdowm());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting) && !isSlowTime)
                setting.MoveSetting.ModValueSpeed.RemoveModifSpeed(speedDown);
        }

        private IEnumerator TimerSlowdowm()
        {
            yield return new WaitForSeconds(timeSlow);
            _setting.MoveSetting.ModValueSpeed.RemoveModifSpeed(speedDown);
        }
    }
}