using Tactics.Units.Data;
using Tactics.Units.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Installers
{
    public class BaseUnitsInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform unitsRoot;
        [SerializeField]
        private UnitDataContainer playerUnitDataPrefab;
        [SerializeField]
        private UnitDataContainer enemyUnitDataPrefab;

        public override void InstallBindings()
        {
            InstallFactories();
        }

        private void InstallFactories()
        {
            InstallFactory(playerUnitDataPrefab, UnitConstants.PlayerUnitFactoryID);
            InstallFactory(enemyUnitDataPrefab, UnitConstants.EnemyUnitFactoryID);
        }

        private void InstallFactory(UnitDataContainer unitPrefab, string factoryID)
        {
            var factory = new BaseUnitFactory(unitPrefab, unitsRoot);
            Container.Inject(factory);
            Container.Bind<BaseUnitFactory>().WithId(factoryID).FromInstance(factory).AsCached();
        }
    }
}
