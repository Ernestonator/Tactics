using System.Collections.Generic;
using Tactics.Graphs.Data.Models;

namespace Tactics.Graphs.Services
{
    public class Grid2D<TTileContent> where TTileContent : GridTile, new()
    {
        private readonly int _height;
        private readonly int _width;
        
        public Graph<TTileContent> Graph { get; private set; }
        
        public int Height => _height;
        public int Width => _width;
        
        public Grid2D(int height, int width)
        {
            _height = height;
            _width = width;

            InitializeGraph();
        }

        private void InitializeGraph()
        {
            var nodes = new List<Node<TTileContent>>();
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    var node = new Node<TTileContent>
                    {
                        Connections = new (),
                        Content = new TTileContent
                        {
                            XIndex = i,
                            YIndex = j,
                        }
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
            for (int i = 0; i < graph.Nodes.Count; i++)
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

        public bool TryFindNodeAt(int x, int y, out Node<TTileContent> node)
        {
            return TryFindNodeAt(Graph, x, y, out node);
        }

        private bool TryFindNodeAt(Graph<TTileContent> graph, int x, int y, out Node<TTileContent> node)
        {
            for (int i = 0; i < graph.Nodes.Count; i++)
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