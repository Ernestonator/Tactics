using Tactics.GameGrid.Implementation.Services;
using Zenject;

namespace Tactics.GameGrid.Installers
{
    public class GridInteractionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GridInteractions>().AsSingle();
        }
    }
}