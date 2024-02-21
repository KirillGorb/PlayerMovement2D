using System.Threading.Tasks;
using Play.Movement.Abstraction;
using UnityEngine;

namespace Play.Block
{
    public class InertionBlock : MonoBehaviour
    {
        [SerializeField] private float speedDir = 2;
        [SerializeField] private float timeAdd = 0.4f;
        private ISettingMoveble _setting;

        private async void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ISettingMoveble settingMoveble))
            {
                _setting = settingMoveble;
                _setting.MoveSetting.IsCanMoveHorizontal.Value = false;
                _setting.JumpSettings.IsCanJump.Value = false;

                _setting.GetRigidbody2D.AddForce(_setting.GetRigidbody2D.velocity * speedDir);

                await Task.Delay((int)(timeAdd * 1000));
                _setting.MoveSetting.IsCanMoveHorizontal.Value = true;
                _setting.JumpSettings.IsCanJump.Value = true;
            }
        }
    }
}