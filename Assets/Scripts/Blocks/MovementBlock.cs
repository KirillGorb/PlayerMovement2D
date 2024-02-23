using System;
using UnityEngine;

namespace Play.Block
{
    public class MovementBlock : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private Transform block;
        [SerializeField] private float speed;

        private float _t;
        private Vector2 nextPoint;
        private int _id;

        private void Start()
        {
            CheckNextPoint();
        }

        private void Update()
        {
            _t += Time.deltaTime * speed;
            block.transform.position = Vector2.Lerp(points[_id].position, nextPoint, _t);

            if (_t >= 1)
            {
                _id = points.Length > _id + 1 ? _id + 1 : 0;
                _t = 0;
                CheckNextPoint();
            }
        }
        
        private void CheckNextPoint()=>
            nextPoint = points[points.Length > _id + 1 ? _id + 1 : 0].position;
    }
}