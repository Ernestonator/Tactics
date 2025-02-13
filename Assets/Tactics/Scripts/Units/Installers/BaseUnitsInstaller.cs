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

        public override void InstallBindings()
        {
            var factory = new BaseUnitFactory(playerUnitDataPrefab, unitsRoot);
            Container.Inject(factory);
            Container.Bind<BaseUnitFactory>().WithId(UnitConstants.PlayerUnitFactoryID).FromInstance(factory).AsSingle();
        }
    }
}
