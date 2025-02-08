using System.Collections.Generic;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using UnityEngine;

namespace Tactics.Units.Services
{
    public interface IUnitMovement
    {
        Vector2Int? LastPosition { get; }
        bool TrySetLogicPosition(Vector2Int position);
        List<Node<GameTile>> GetTilesInRange();
        bool IsTileReachable(Vector2Int position);
        List<Node<GameTile>> CalculatePath(Vector2Int target);
    }
}