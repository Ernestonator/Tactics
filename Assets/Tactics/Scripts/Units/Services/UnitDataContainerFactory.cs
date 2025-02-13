using UnityEngine;

namespace Tactics.Units.Services
{
    public class UnitDataContainerFactory
    {
        private readonly UnitDataContainer _unitDataContainerPrefab;

        public UnitDataContainerFactory(UnitDataContainer unitDataContainerPrefab)
        {
            _unitDataContainerPrefab = unitDataContainerPrefab;
        }

        public UnitDataContainer Create(Transform parent)
        {
            var unitContainer = Object.Instantiate(_unitDataContainerPrefab, parent);
            return unitContainer;
        }
    }
}
