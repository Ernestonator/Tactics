using Tactics.Units.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Installers
{
    public class BaseUnitsInstaller : MonoInstaller
    {
        [SerializeField] private Transform unitsRoot;
        [SerializeField] private UnitDataContainer unitDataPrefab;
        
        public override void InstallBindings()
        {
            var factory = new BaseUnitFactory(unitDataPrefab, unitsRoot);
            Container.BindInterfacesAndSelfTo<BaseUnitFactory>().FromInstance(factory).AsSingle();
        }
    }
}