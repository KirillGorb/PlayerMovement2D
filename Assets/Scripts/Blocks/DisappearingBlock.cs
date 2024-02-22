using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Play.Block
{
    public class DisappearingBlock : MonoBehaviour
    {
        [SerializeField] private UnityEvent disActive;
        [SerializeField] private UnityEvent active;
        [SerializeField] private float timeDisActive;
        [SerializeField] private float timeActive;

        private bool _isDisActive;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isDisActive)
            {
                _isDisActive = true;
                StartCoroutine(Timer());
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(timeDisActive);
            disActive?.Invoke();
            yield return new WaitForSeconds(timeActive);
            active?.Invoke();
            _isDisActive = false;
        }
    }
}