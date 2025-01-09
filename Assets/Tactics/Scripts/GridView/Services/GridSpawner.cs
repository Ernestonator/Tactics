using Tactics.Graphs.Services;
using Tactics.GridView.Data.Models;
using UnityEngine;
using Zenject;

namespace Tactics.GridView.Services
{
    public abstract class GridSpawner<TTileData> : IInitializable where TTileData : TileData, new()
    {
        private readonly int _gridSize;
        private readonly float _tileSize;
        private readonly GameObject _tilePrefab;
        private readonly Transform _tilesRoot;
        
        private Grid2D<TTileData> _grid2D;

        public Grid2D<TTileData> Grid => _grid2D;

        protected GridSpawner(int gridSize, float tileSize, GameObject tilePrefab, Transform tilesRoot)
        {
            _gridSize = gridSize;
            _tileSize = tileSize;
            _tilePrefab = tilePrefab;
            _tilesRoot = tilesRoot;
        }

        public void Initialize()
        {
            _grid2D = new Grid2D<TTileData>(_gridSize, _gridSize);
            SpawnTiles();
        }

        private void SpawnTiles()
        {
            var gridCenter = new Vector2(_gridSize / 2f, _gridSize / 2f);
            for (int i = 0; i < _grid2D.Graph.Nodes.Count; i++)
            {
                var tile = Object.Instantiate(_tilePrefab, _tilesRoot);
                var node = _grid2D.Graph.Nodes[i];
                node.Content.TileView = new TileView()
                {
                    TileGameObject = tile
                };
                InitializeContent(node);
                var xPos = node.Content.XIndex * _tileSize - gridCenter.x;
                var zPos = node.Content.YIndex * _tileSize - gridCenter.y;
                tile.transform.position = new Vector3(xPos,tile.transform.position.y, zPos);
            }
        }

        protected abstract void InitializeContent(Node<TTileData> node);
    }
}