using System.Collections.Generic;
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

        private readonly Dictionary<string, BaseUnitFactory> _factories = new();

        public override void InstallBindings()
        {
            InstallFactoriesDictionary();
        }

        private void InstallFactoriesDictionary()
        {
            CreateAndAddFactory(playerUnitDataPrefab, UnitConstants.PlayerUnitFactoryID);
            CreateAndAddFactory(enemyUnitDataPrefab, UnitConstants.EnemyUnitFactoryID);

            Container.Bind<Dictionary<string, BaseUnitFactory>>().FromInstance(_factories).AsSingle();
        }

        private void CreateAndAddFactory(UnitDataContainer unitPrefab, string factoryID)
        {
            var factory = new BaseUnitFactory(unitPrefab, unitsRoot);
            Container.Inject(factory);
            _factories[factoryID] = factory;
        }
    }
}
