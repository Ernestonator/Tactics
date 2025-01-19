using Tactics.GameGrid.Sandbox.Services;
using Zenject;

namespace Tactics.GameGrid.Sandbox.Installers
{
    public class GameGridSandboxInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DebugTileIndexes>().AsSingle();
            Container.BindInterfacesAndSelfTo<LogHoverInteractions>().AsSingle();
        }
    }
}