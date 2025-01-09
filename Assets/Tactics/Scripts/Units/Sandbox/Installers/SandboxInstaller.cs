using Tactics.Units.Sandbox.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Sandbox.Installers
{
    public class SandboxInstaller : MonoInstaller
    {
        [SerializeField]
        BaseUnitSpawner _baseUnitSpawner;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BaseUnitSpawner>().FromInstance(_baseUnitSpawner).AsSingle();
        }
    }
}