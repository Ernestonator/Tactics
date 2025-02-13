using System.Collections.Generic;
using Tactics.Graphs.Data.Models;
using UnityEngine;

namespace Tactics.Graphs.Services
{
    public class Grid2D<TTileContent> where TTileContent : GridTile, new()
    {
        public Graph<TTileContent> Graph { get; private set; }

        public int Height { get; }
        public int Width { get; }

        public Grid2D(int height, int width)
        {
            Height = height;
            Width = width;

            InitializeGraph();
        }

        public bool TryFindNodeAt(Vector2Int position, out Node<TTileContent> node)
        {
            return TryFindNodeAt(position.x, position.y, out node);
        }

        public bool TryFindNodeAt(int x, int y, out Node<TTileContent> node)
        {
            return TryFindNodeAt(Graph, x, y, out node);
        }

        private void InitializeGraph()
        {
            var nodes = new List<Node<TTileContent>>();

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    var node = new Node<TTileContent>
                    {
                        Connections = new List<Node<TTileContent>>(),
                        Content = new TTileContent
                        {
                            XIndex = i,
                            YIndex = j,
                        },
                    };

                    nodes.Add(node);
                }
            }

            var graph = new Graph<TTileContent>(nodes);
            InitializeNeighbours(graph);
            Graph = graph;
        }

        private void InitializeNeighbours(Graph<TTileContent> graph)
        {
            for (var i = 0; i < graph.Nodes.Count; i++)
            {
                var node = graph.Nodes[i];
                var x = node.Content.XIndex;
                var y = node.Content.YIndex;

                if (TryFindNodeAt(graph, x - 1, y, out var leftNeighbour))
                {
                    node.Connections.Add(leftNeighbour);
                }

                if (TryFindNodeAt(graph, x + 1, y, out var rightNeighbour))
                {
                    node.Connections.Add(rightNeighbour);
                }

                if (TryFindNodeAt(graph, x, y + 1, out var topNeighbour))
                {
                    node.Connections.Add(topNeighbour);
                }

                if (TryFindNodeAt(graph, x, y - 1, out var bottomNeighbour))
                {
                    node.Connections.Add(bottomNeighbour);
                }
            }
        }

        private bool TryFindNodeAt(Graph<TTileContent> graph,
            int x,
            int y,
            out Node<TTileContent> node)
        {
            for (var i = 0; i < graph.Nodes.Count; i++)
            {
                node = graph.Nodes[i];

                if (node.Content.XIndex == x && node.Content.YIndex == y)
                {
                    return true;
                }
            }

            node = null;
            return false;
        }
    }
}
