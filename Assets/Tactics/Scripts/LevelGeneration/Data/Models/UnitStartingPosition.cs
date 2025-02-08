using System;
using Tactics.LevelGeneration.Data.Enums;
using UnityEngine;

namespace Tactics.LevelGeneration.Data.Models
{
    [Serializable]
    internal class UnitStartingPosition
    {
        [field: SerializeField] internal UnitType UnitType { get; private set; }
        [field: SerializeField] internal Vector2Int UnitPosition { get; private set; }
    }
}