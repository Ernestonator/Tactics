using System.Collections.Generic;
using System.Linq;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using UnityEngine;

namespace Tactics.GameGrid.Implementation.Extensions
{
    public static class NodesConverter
    {
        public static List<Vector2Int> ToIndexes(this List<Node<GameTile>> nodes)
        {
            return nodes.Select(x => new Vector2Int(x.Content.XIndex, x.Content.YIndex)).ToList();
        }
    }
}
