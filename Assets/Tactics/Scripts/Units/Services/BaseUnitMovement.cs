using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using Tactics.Units.Data.Models;
using UnityEngine;

namespace Tactics.Units.Services
{
    public class BaseUnitMovement : IUnitMovement
    {
        private readonly Grid2D<GameTile> _grid;
        private readonly MovementParameters _movementParameters;
        
        private Vector2Int _lastPosition;
        
        public BaseUnitMovement(Grid2D<GameTile> grid, MovementParameters movementParameters)
        {
            _grid = grid;
            _movementParameters = movementParameters;
        }

        public bool TrySetLogicPosition(Vector2Int position)
        {
            if (!_grid.TryFindNodeAt(position.x, position.y, out var node))
            {
                return false;
            }

            if (node.Content.IsOccupied())
            {
                return false;
            }
            
            _lastPosition = position;
            node.Content.SetOccupied(true);
            return true;
        }

        public List<Node<GameTile>> GetTilesInRange()
        {
            var bfs = new BFS<GameTile>(_grid);
            return bfs.CalculateAllReachableTiles(_lastPosition, _movementParameters.Range);
        }

        public void CalculatePath(Vector2Int target)
        {
            throw new System.NotImplementedException();
        }

        public UniTask PerformMovementAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}