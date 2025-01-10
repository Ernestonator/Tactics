using Tactics.InputSystem.Services;
using Zenject;

namespace Tactics.InputSystem.Installers
{
    public class InputSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputSystem_Actions>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputsProvider>().AsSingle();
        }
    }
}