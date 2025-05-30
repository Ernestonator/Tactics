﻿using System;
using Tactics.GameGrid.Implementation.Services;
using Tactics.HealthSystem.Implementation.Services;
using Tactics.Units.Services;
using Zenject;
using Object = UnityEngine.Object;

namespace Tactics.PlayerUnits.Services
{
    public class BaseUnitFacade : IDisposable
    {
        [Inject]
        private IGameGridProvider _gameGridProvider;

        public UnitDataContainer UnitDataContainer { get; private set; }
        public IUnitMovement UnitMovement { get; private set; }
        public IHealthTarget HealthTarget { get; private set; }

        public void Dispose()
        {
            Object.Destroy(UnitDataContainer.UnitGameObject);
        }

        [Inject]
        public virtual void Construct(UnitDataContainer unitDataContainer)
        {
            UnitDataContainer = unitDataContainer;
            UnitMovement = new BaseUnitMovement(_gameGridProvider.Grid, unitDataContainer.MovementParameters);
            HealthTarget = new HealthTarget(unitDataContainer.HealthParameters);
        }
    }
}
