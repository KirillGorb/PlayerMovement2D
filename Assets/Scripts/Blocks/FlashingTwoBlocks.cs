using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Block
{
    public class FlashingTwoBlocks : MonoBehaviour
    {
        [SerializeField] private GameObject obj1;
        [SerializeField] private GameObject obj2;

        [SerializeField] private float timeActive;

        private void Start()
        {
            StartCoroutine(Timer());
        }

        private void OnActive(bool active)
        {
            obj1.SetActive(active);
            obj2.SetActive(!active);
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                OnActive(true);
                yield return new WaitForSeconds(timeActive);
                OnActive(false);
                yield return new WaitForSeconds(timeActive);
            }
        }
    }
}