using Play.Input;
using Play.Movement.Abstraction;
using Play.Movement.Setting;
using UnityEngine;
using Zenject;

namespace Play.Movement.Controller
{
    public class MovementInjector : MonoInstaller
    {
        [SerializeField] private CheckInput input;
        [SerializeField] private MoveController moveController;

        public override void InstallBindings()
        {
            Container.Bind<CheckInput>().FromInstance(input);
            Container.Bind<ISettingMoveble>().FromInstance(moveController);
        }
    }
}