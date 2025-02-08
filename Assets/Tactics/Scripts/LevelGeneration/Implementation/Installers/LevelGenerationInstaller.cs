using Tactics.LevelGeneration.Data.Settings;
using Tactics.LevelGeneration.Implementation.Services;
using UnityEngine;
using Zenject;

namespace Tactics.LevelGeneration.Implementation.Installers
{
    public class LevelGenerationInstaller : MonoInstaller
    {
        [SerializeField] private LevelLayout _levelLayout;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitFactoriesMap>().AsSingle();    
            Container.BindInterfacesAndSelfTo<LevelLayoutSpawner>().AsSingle().WithArguments(_levelLayout);    
        }   
    }
}