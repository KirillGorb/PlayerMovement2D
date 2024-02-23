using Play.Movement.Abstraction;
using Play.Input;
using UnityEngine;
using System.Collections;

namespace Play.Block
{
    public class FallDownBlock : MonoBehaviour
    {
        [SerializeField] private float timeToFall;
        [SerializeField] private float gravityFall;
        [SerializeField] private Rigidbody2D rd;

        private bool _isActive = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_isActive && collision.gameObject.GetComponent<ISettingMoveble>() == null)
            {
                rd.gravityScale = 0;
                Destroy(this);
                Destroy(rd);
            }

            if (collision.gameObject.TryGetComponent(out ISettingMoveble setting) &&
                setting.IsValueUpPlayer(transform))
                StartCoroutine(TimerFall());
        }

        private IEnumerator TimerFall()
        {
            yield return new WaitForSeconds(timeToFall);
            rd.bodyType = RigidbodyType2D.Dynamic;
            rd.gravityScale = gravityFall;
            _isActive = true;
        }
    }
}