using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Movement.Abstraction;

namespace Play.Block
{
    public class SinkingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform max;
        [SerializeField] private Transform min;
        [SerializeField] private Transform block;

        [SerializeField] private float speed;

        private float _t;
        private bool _isUp;

        private void Update()
        {
            if (_isUp)
            {
                _t -= speed * Time.deltaTime;
                block.transform.position = Vector2.Lerp(max.position, min.position, _t);
                if (_t <= 0)
                    _isUp = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>())
            {
                _t += speed * Time.deltaTime;
                block.transform.position = Vector2.Lerp(max.position, min.position, _t);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>())
                _isUp = true;
        }
    }
}