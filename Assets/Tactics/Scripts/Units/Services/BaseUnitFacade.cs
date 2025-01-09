using System;
using Tactics.GameGrid.Implementation.Services;
using Tactics.Units.Services;
using Zenject;
using Object = UnityEngine.Object;

namespace Tactics.PlayerUnits.Services
{
    public class BaseUnitFacade : IDisposable
    {
        [Inject]
        GameGridSpawner _gridSpawner;
     
        public UnitDataContainer UnitDataContainer { get; }   
        public IUnitMovement UnitMovement { get; private set; }
        
        public BaseUnitFacade(UnitDataContainer unitDataContainer)
        {
            UnitDataContainer = unitDataContainer;
            UnitMovement = new BaseUnitMovement(_gridSpawner.Grid, unitDataContainer.MovementParameters);
        }

        public void Dispose()
        {
            Object.Destroy(UnitDataContainer.UnitGameObject);
        }
    }
}