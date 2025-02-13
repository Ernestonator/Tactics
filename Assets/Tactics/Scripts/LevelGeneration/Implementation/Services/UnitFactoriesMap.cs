using System;
using System.Collections.Generic;
using Tactics.LevelGeneration.Data.Enums;
using Tactics.Units.Data;
using Tactics.Units.Services;
using Zenject;

namespace Tactics.LevelGeneration.Implementation.Services
{
    internal class UnitFactoriesMap
    {
        [Inject]
        private Dictionary<string, BaseUnitFactory> _unitFactories;

        internal BaseUnitFactory GetFactory(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Player:
                    return _unitFactories[UnitConstants.PlayerUnitFactoryID];
                case UnitType.Enemy:
                    return _unitFactories[UnitConstants.EnemyUnitFactoryID];
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null);
            }
        }
    }
}
