using System;
using System.Collections.Generic;
using Tactics.Graphs.Data.Models;
using UnityEngine;

namespace Tactics.Graphs.Services
{
    public class BFS<TTileContent> where TTileContent : GridTile, new()
    {
        private readonly Grid2D<TTileContent> _grid;

        public BFS(Grid2D<TTileContent> grid)
        {
            _grid = grid;
        }

        public List<Node<TTileContent>> CalculateAllReachableTiles(Vector2Int startPoint, int range)
        {
            if (!_grid.TryFindNodeAt(startPoint.x, startPoint.y, out var startingNode))
            {
                throw new Exception($"{nameof(BFS<TTileContent>)} shouldn't operate on invalid node x, y.");
            }
            
            var visitedTiles = new List<Node<TTileContent>>();
            visitedTiles.Add(startingNode);

            Queue<(Vector2Int, int)> queue = new Queue<(Vector2Int, int)>();
            queue.Enqueue((startPoint, range));

            while (queue.Count > 0)
            {
                var (currentPoint, movesLeft) = queue.Dequeue();

                if (movesLeft <= 0)
                {
                    continue;
                }

                if (!_grid.TryFindNodeAt(currentPoint.x, currentPoint.y, out var node))
                {
                    throw new Exception($"{nameof(BFS<TTileContent>)} shouldn't operate on invalid node x, y.");
                }

                for (int i = 0; i < node.Connections.Count; i++)
                {
                    var neighbourNode = node.Connections[i];
                    var (nx, ny) = (neighbourNode.Content.XIndex, neighbourNode.Content.YIndex);

                    var tileInRange = nx >= 0 && nx < _grid.Width && ny >= 0 && ny < _grid.Height;
                    var tileNotOccupied = !neighbourNode.Content.IsOccupied();
                    var tileNotVisited = !visitedTiles.Contains(neighbourNode);
                    if (tileInRange && tileNotOccupied && tileNotVisited)
                    {
                        visitedTiles.Add(neighbourNode);
                        queue.Enqueue((new Vector2Int(nx, ny), movesLeft - 1));
                    }
                }
            }
            
            return visitedTiles;
        }
    }
}