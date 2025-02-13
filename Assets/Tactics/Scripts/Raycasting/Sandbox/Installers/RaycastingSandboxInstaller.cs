using Tactics.Raycasting.Sandbox.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Raycasting.Sandbox.Installers
{
    public class RaycastingSandboxInstaller : MonoInstaller
    {
        [SerializeField]
        private TestInteractionService testInteractionService;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TestInteractionService>().FromComponentInNewPrefab(testInteractionService).AsSingle();
        }
    }
}
