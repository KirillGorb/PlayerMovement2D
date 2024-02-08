using UnityEngine;

namespace Play.Movement.Controller
{
    public class CheckGroundController : MonoBehaviour
    {
        [SerializeField] private Transform checkGroundPoint;
        [SerializeField] private float radius;
        [SerializeField] private float _groundExitCooldown;
        [SerializeField] private LayerMask layerGround;

        private float _lastGroundExitTime;

        private bool _isGround;

        public bool IsConectGround => Physics2D.OverlapCircle(checkGroundPoint.position, radius, layerGround);

        public bool CheckGround
        {
            get
            {
                _isGround = IsConectGround;

                if (!_isGround)
                {
                    if (Time.time - _lastGroundExitTime < _groundExitCooldown)
                        _isGround = true;
                }
                else
                {
                    _lastGroundExitTime = Time.time;
                }
                return _isGround;
            }
        }
    }
}