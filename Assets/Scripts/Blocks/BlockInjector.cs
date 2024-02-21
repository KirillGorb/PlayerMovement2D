using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class BlockInjector : MonoInstaller
    {
        [SerializeField] private Rotate3DUpdater updater;

        public override void InstallBindings()
        {
            Container.Bind<Rotate3DUpdater>().FromInstance(updater);
        }
    }
}