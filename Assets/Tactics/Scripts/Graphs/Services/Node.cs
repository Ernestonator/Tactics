using System.Collections.Generic;

namespace Tactics.Graphs.Services
{
    public class Node<TContent>
    {
        public TContent Content { get; set; }
        public List<Node<TContent>> Connections { get; set; }
    }
}
