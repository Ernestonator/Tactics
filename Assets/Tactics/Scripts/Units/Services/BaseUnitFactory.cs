using Tactics.PlayerUnits.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Services
{
    public class BaseUnitFactory
    {
        private readonly UnitDataContainerFactory _unitDataContainerFactory;
        private readonly Transform _parent;
        
        [Inject]
        DiContainer _container;

        public BaseUnitFactory(UnitDataContainer prefab, Transform parent)
        {
            _unitDataContainerFactory = new UnitDataContainerFactory(prefab);
            _parent = parent;
        }
        
        public BaseUnitFacade Create()
        {
            var dataContainer = _unitDataContainerFactory.Create(_parent);
            var unit = _container.Instantiate<BaseUnitFacade>(new [] {dataContainer});
            return unit;
        }
    }
}