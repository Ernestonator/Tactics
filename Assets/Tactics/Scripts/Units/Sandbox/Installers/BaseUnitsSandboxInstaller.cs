using Tactics.Units.Sandbox.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Sandbox.Installers
{
    public class BaseUnitsSandboxInstaller : MonoInstaller
    {
        [SerializeField]
        private BaseUnitSpawner baseUnitSpawner;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BaseUnitSpawner>().FromInstance(baseUnitSpawner).AsSingle();
        }
    }
}
