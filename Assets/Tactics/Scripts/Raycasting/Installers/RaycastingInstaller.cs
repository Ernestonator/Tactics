using Tactics.Raycasting.Services;
using Zenject;

namespace Tactics.Raycasting.Installers
{
    public class RaycastingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RayCaster>().AsSingle();
        }
    }
}