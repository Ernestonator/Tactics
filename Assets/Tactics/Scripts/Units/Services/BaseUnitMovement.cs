using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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
        
        private Vector2Int? _lastPosition;
        private List<Node<GameTile>> _currentlyCalculatedPath;
        
        public BaseUnitMovement(Grid2D<GameTile> grid, MovementParameters movementParameters)
        {
            _grid = grid;
            _movementParameters = movementParameters;
        }

        public bool TrySetLogicPosition(Vector2Int position)
        {
            if (_lastPosition.HasValue)
            {
                if (!_grid.TryFindNodeAt(position.x, position.y, out var lastNode))
                {
                    Debug.LogError($"Could not find last node at {position}");
                    return false;
                } 
                
                lastNode.Content.SetOccupied(false);
            }
            
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
            if (!_lastPosition.HasValue)
            {
                Debug.LogError("Can't get tiles in range with no last position set.");
                return null;
            }
            
            var bfs = new BFS<GameTile>(_grid);
            return bfs.CalculateAllReachableTiles(_lastPosition.Value, _movementParameters.Range);
        }

        [CanBeNull]
        public List<Node<GameTile>> CalculatePath(Vector2Int target)
        {
            if (!_lastPosition.HasValue)
            {
                Debug.LogError("Can't calculate path with no last position set.");
                return null;
            }
            
            var bfs = new BFS<GameTile>(_grid);
            if (!bfs.TryCalculateShortestPath(_lastPosition.Value, target, _movementParameters.Range, out var path))
            {
                return null;
            }
            return path;
        }

        public UniTask PerformMovementAsync()
        {
            if (_currentlyCalculatedPath == null)
            {
                Debug.LogError("No set action to perform.");
            }
            
            // TODO perform animation
            _currentlyCalculatedPath = null;
            return UniTask.CompletedTask;
        }
    }
}