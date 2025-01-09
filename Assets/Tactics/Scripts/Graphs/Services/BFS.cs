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

        // TODO: validate if it works corretly, seems to work wrong.
        // https://chatgpt.com/c/67795f39-edfc-800c-a680-29218a354099
        public bool TryCalculateShortestPath(Vector2Int startPoint, Vector2Int targetPoint, int range,
            out List<Node<TTileContent>> path)
        {
            if (!_grid.TryFindNodeAt(startPoint.x, startPoint.y, out var startNode))
            {
                Debug.LogError("Can't operate outside of board.");
                path = null;
                return false;
            }
            
            if (!_grid.TryFindNodeAt(targetPoint.x, targetPoint.y, out var endNode))
            {
                Debug.LogError("Can't operate outside of board.");
                path = null;
                return false;
            }
            
            path = new();

            if (startPoint.x == targetPoint.x && startPoint.y == targetPoint.y)
            {
                path.Add(startNode);
                return true;
            }
            
            var visitedTiles = new List<Node<TTileContent>>();
            visitedTiles.Add(startNode);
            
           var queue = new Queue<(Vector2Int, List<Node<TTileContent>>)>();
            queue.Enqueue((startPoint, new () { startNode }));

            while (queue.Count > 0)
            {
                var (currentPoint, currentPath) = queue.Dequeue();
                
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
                        var newPath = new List<Node<TTileContent>>(currentPath) { node };

                        if ((nx, ny) == (endNode.Content.XIndex, endNode.Content.YIndex))
                        {
                            if (newPath.Count - 1 <= range)
                            {
                                path = newPath;
                                return true;
                            }
                            else
                            {
                                return false; 
                            }
                        }
                        
                        visitedTiles.Add(neighbourNode);
                        queue.Enqueue((new Vector2Int(nx, ny), newPath));
                    }
                }
            }

            path = null;
            return false;
        }
    }
}