using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using UnityEngine;

namespace Tactics.Units.Services
{
    public interface IUnitMovement
    {
        void SetLogicPosition(Vector2Int position);
        List<Node<GameTile>> GetTilesInRange();
        void CalculatePath(Vector2Int target);
        UniTask PerformMovementAsync();
    }
}