using Tactics.Common.Implementation.Services;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using Tactics.Raycasting.Services;
using UnityEngine;

namespace Tactics.GameGrid.Implementation.Services
{
    public class InteractableTile : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public GameObject GameObject { get; private set; }
        [field: SerializeField]
        public SerializedInterface<IGameTileView> GameTileComponent { get; private set; }
        public Node<GameTile> Node { get; private set; }

        internal void SetNode(Node<GameTile> node)
        {
            Node = node;
        }
    }
}