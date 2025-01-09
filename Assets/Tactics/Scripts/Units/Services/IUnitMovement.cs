using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using UnityEngine;

namespace Tactics.Units.Services
{
    public interface IUnitMovement
    {
        List<Node<GameTile>> GetTilesInRange();
        void CalculatePath(Vector2Int target);
        UniTask PerformMovementAsync();
    }
}