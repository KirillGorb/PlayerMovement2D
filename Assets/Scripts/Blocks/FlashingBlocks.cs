using System.Collections;
using UnityEngine;

namespace Play.Block
{
    public class FlashingBlocks : MonoBehaviour
    {
        [SerializeField] private GameObject[] objs1;
        [SerializeField] private GameObject[] objs2;

        [SerializeField] private float timeActive;

        private void Start()
        {
            StartCoroutine(Timer());
        }

        private void OnActive(bool active)
        {
            for (int i = 0; i < objs2.Length; i++)
            {
                objs1[i].SetActive(active);
                objs2[i].SetActive(!active);
            }
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