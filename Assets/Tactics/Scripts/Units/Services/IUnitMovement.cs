using System.Collections.Generic;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using UnityEngine;

namespace Tactics.Units.Services
{
    public interface IUnitMovement
    {
        bool TrySetLogicPosition(Vector2Int position);
        List<Node<GameTile>> GetTilesInRange();
        List<Node<GameTile>> CalculatePath(Vector2Int target);
    }
}