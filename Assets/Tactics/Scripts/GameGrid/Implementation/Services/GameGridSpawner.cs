using Tactics.GameGrid.Data.Models;
using Tactics.GameGrid.Implementation.Components;
using Tactics.Graphs.Services;
using Tactics.GridView.Services;
using UnityEngine;

namespace Tactics.GameGrid.Implementation.Services
{
    public class GameGridSpawner : GridSpawner<GameTile>
    {
        public GameGridSpawner(int gridSize, float tileSize, GameObject tilePrefab, Transform tilesRoot) : base(gridSize, tileSize, tilePrefab, tilesRoot)
        {
        }

        protected override void InitializeContent(Node<GameTile> node)
        {
            node.Content.GameTileComponent = node.Content.TileView.TileGameObject.GetComponent<GameTileComponent>();
        }
    }
}