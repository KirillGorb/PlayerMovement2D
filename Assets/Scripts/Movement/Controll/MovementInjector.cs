using Play.Input;
using Play.Movement.Abstraction;
using Play.Movement.Setting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Play.Movement.Controller
{
    public class MovementInjector : MonoInstaller
    {
        [FormerlySerializedAs("input")] [SerializeField] private InputCenter inputCenter;
        [SerializeField] private MoveController moveController;

        public override void InstallBindings()
        {
            Container.Bind<InputCenter>().FromInstance(inputCenter);
            Container.Bind<ISettingMoveble>().FromInstance(moveController);
        }
    }
}