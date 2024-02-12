using Play.Input;
using Play.Movement.Abstraction;
using System.Collections;
using UnityEngine;

namespace Play.Block
{
    public class BlackBlock : MonoBehaviour
    {
        [SerializeField] private CheckInput input;
        [Space]
        [SerializeField] private float maxRadiyse = 4;
        [SerializeField] private float speedDist = 0.2f;
        [Space]
        [SerializeField] private float _k_speedDrop = 3;
        [SerializeField] private float _k_speedMax = 6;
        [SerializeField] private float _k_speedOverRadiyse = 3.5f;
        [SerializeField] private float _k_speedDropDistancy = 6;
        [SerializeField] private float _k_minSpeed= 6;

        private Rigidbody2D _playerRD;
        private Transform _playerPos;
        private ISettingMoveble _setting;

        private float _dist = 1;
        private float _angel = 90;
        private float _steps = 0;
        private float _stepsMin = 3.3f;
        private float _offset = 89.82f;
        private float _stepMax = 6;
        private float _speed = 2;
        private float _maxRadiyse = 4;
        private float _speedUpDististancy = 0.2f;

        private Vector2 _startPos;

        private bool _isConect = false;
        private static bool IsConectPLayer;

        public float Radiuse => _maxRadiyse / _speed;

        private void Start()
        {
            IsConectPLayer = false;
            input.JumpInput.Subscribe(e => { if (e && _isConect) StartCoroutine(TimerDisconnect()); });
        }

        private void Update()
        {
            if (!_isConect) return;

            if (_dist < _maxRadiyse)
                _dist += Time.deltaTime * _speedUpDististancy;
            else if (_dist >= _maxRadiyse)
                _dist -= Time.deltaTime * _speedUpDististancy;

            _steps += Time.deltaTime * _speed;
            if (_steps > _stepMax)
                _steps = 0;
            _startPos = transform.position;
            var velocity = Eleps(_maxRadiyse, _dist, _steps, _startPos, _steps >= _stepsMin ? _angel : _angel + _offset, _steps >= _stepsMin) - (Vector2)_playerPos.position;
            _playerRD.velocity = velocity;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ISettingMoveble setting) && !IsConectPLayer && !_isConect)
            {
                _setting = setting;
                _setting.JumpSettings.IsCanJump.Value = false;
                _setting.IsCanMoveHorizontal.Value = false;
                _playerRD = _setting.GetRigidbody2D;
                _playerPos = _setting.GetTransform;

                _speed = Mathf.Min(_playerRD.velocity.magnitude / _k_speedDrop, _k_minSpeed);
                _dist = Vector2.Distance(transform.position, collision.transform.position) / Mathf.Abs(_k_speedMax - _speed);
                var dir = transform.position - collision.transform.position;
                _angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                _maxRadiyse = maxRadiyse + _speed * _k_speedOverRadiyse;
                _speedUpDististancy = speedDist + _speed / _k_speedDropDistancy;

                IsConectPLayer = true;
                _isConect = true;
            }
        }

        //#if UNITY_EDITOR
        //        private void OnDrawGizmos()
        //        {
        //            Gizmos.color = Color.red;
        //            Vector2 nextPos = Eleps(Radiuse, _dist, 0, _startPos, _angel);
        //            for (float i = 0; i < _stepMax; i += Time.deltaTime * _speed)
        //            {
        //                Vector2 newPos = Eleps(Radiuse, _dist, i, _startPos, i > _stepsMin ? _angel : _angel + _offset, i > _stepsMin);
        //                Gizmos.DrawLine(nextPos, newPos);
        //                nextPos = newPos;
        //            }
        //        }
        //#endif

        private Vector2 Eleps(float a, float b, float m, Vector2 pos, float k, bool isMin = false)
        {
            float dx = a * Mathf.Cos(m);
            float dy = b * Mathf.Sin(m);

            float s = Mathf.Sqrt(dx * dx + dy * dy);
            float F = Mathf.Acos(dx / s);
            float f = isMin ? 360 - F : F;

            float x1 = pos.x + s * Mathf.Cos(f + k);
            float y1 = pos.y + s * Mathf.Sin(f + k);
            return new Vector2(x1, y1);
        }

        private IEnumerator TimerDisconnect()
        {
            _isConect = false;
            yield return new WaitForSeconds(_playerRD.velocity.magnitude / (_setting.JumpSettings.GravityDownMove * 2.5f));
            _setting.JumpSettings.IsCanJump.Value = true;
            _setting.IsCanMoveHorizontal.Value = true;
            IsConectPLayer = false;
        }

        private void OnDestroy()
        {

        }
    }
}