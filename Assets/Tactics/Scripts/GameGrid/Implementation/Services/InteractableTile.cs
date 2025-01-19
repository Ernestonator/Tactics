using Tactics.Common.Implementation.Services;
using Tactics.Raycasting.Services;
using UnityEngine;

namespace Tactics.GameGrid.Implementation.Services
{
    public class InteractableTile : InteractableComponent
    {
        [field: SerializeField]
        public SerializedInterface<IGameTileView> GameTileComponent { get; private set; }
    }
}