using UnityEngine;
using Zenject;

namespace Play.Block
{
    public class BlockInjector : MonoInstaller
    {
        [SerializeField] private Rotate3DUpdater updater;
        [SerializeField] private MoveGropUpdater updaterMovePlayer;

        public override void InstallBindings()
        {
            Container.Bind<Rotate3DUpdater>().FromInstance(updater);
            Container.Bind<MoveGropUpdater>().FromInstance(updaterMovePlayer);
        }
    }
}