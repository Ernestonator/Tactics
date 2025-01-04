using System.Collections.Generic;

namespace Tactics.Graphs.Services
{
    public class Graph<TContent>
    {
        public List<Node<TContent>> Nodes { get; set; }

        public Graph(List<Node<TContent>> nodes)
        {
            Nodes = nodes;
        }
    }
}
