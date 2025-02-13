using Tactics.Players.Services;
using Zenject;

namespace Tactics.Players.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInteractions>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInteractionBehavior>().AsSingle();
        }
    }
}
