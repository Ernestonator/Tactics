using Tactics.GameGrid.Data.Models;
using Tactics.GameGrid.Implementation.Components;
using Tactics.Graphs.Services;
using Tactics.GridView.Services;
using UnityEngine;

namespace Tactics.GameGrid.Implementation.Services
{
    internal class GameGridSpawner : GridSpawner<GameTile>, IGameGridProvider
    {
        public GameGridSpawner(int gridSize, float tileSize, GameObject tilePrefab, Transform tilesRoot) : base(gridSize, tileSize, tilePrefab, tilesRoot)
        {
        }

        protected override void InitializeContent(Node<GameTile> node)
        {
            node.Content.GameTileView = node.Content.TileView.TileGameObject.GetComponent<IGameTileView>();
            node.Content.TileView.TileGameObject.name = $"TileView ({node.Content.XIndex}, {node.Content.YIndex})";
        }
    }
}