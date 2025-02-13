using Tactics.GameGrid.Sandbox.Services;
using UnityEngine;
using Zenject;

namespace Tactics.GameGrid.Sandbox.Installers
{
    public class GameGridSandboxInstaller : MonoInstaller
    {
        [SerializeField]
        private bool logHoverInteractions;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DebugTileIndexes>().AsSingle();

            if (logHoverInteractions)
            {
                Container.BindInterfacesAndSelfTo<LogHoverInteractions>().AsSingle();
            }
        }
    }
}
