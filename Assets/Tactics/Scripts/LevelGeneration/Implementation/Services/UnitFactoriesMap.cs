using System;
using Tactics.LevelGeneration.Data.Enums;
using Tactics.Units.Data;
using Tactics.Units.Services;
using Zenject;

namespace Tactics.LevelGeneration.Implementation.Services
{
    internal class UnitFactoriesMap
    {
        [Inject(Id = UnitConstants.PlayerUnitFactoryID)]
        private BaseUnitFactory _playerUnitFactory;

        internal BaseUnitFactory GetFactory(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Player:
                    return _playerUnitFactory;
                case UnitType.Enemy:
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null);
            }
        }
    }
}
