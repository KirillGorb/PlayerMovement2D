using Play.Input;
using Play.Movement.Setting;
using UnityEngine;
using Zenject;

namespace Play.Movement.Controller
{
    public class MovementInjector : MonoInstaller
    {
        [SerializeField] private CheckInput input;
        [SerializeField] private JumpSetting jumpSetting;
        [SerializeField] private MovementHorizontalSetting horizontalSetting;

        public override void InstallBindings()
        {
            Container.Bind<CheckInput>().FromInstance(input);
            Container.Bind<JumpSetting>().FromInstance(jumpSetting);
            Container.Bind<MovementHorizontalSetting>().FromInstance(horizontalSetting);
        }
    }
}