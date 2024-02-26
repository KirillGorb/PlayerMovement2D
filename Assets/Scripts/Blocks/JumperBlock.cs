using System.Collections;
using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public class JumperBlock : MonoBehaviour
    {
        [SerializeField] private float force;
        [SerializeField] private float timeFly;

        private ISettingMoveble _setting;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble setting))
            {
                _setting = setting;
                _setting.JumpSettings.Activator.OnDisactiveMove(this);
                _setting.GetRigidbody2D.velocity = new Vector2(_setting.GetRigidbody2D.velocity.x, force);
                StartCoroutine(TimeActive());
            }
        }

        private IEnumerator TimeActive()
        {
            yield return new WaitForSeconds(timeFly);
            _setting.JumpSettings.Activator.OnActiveMove(this);
        }
    }
}