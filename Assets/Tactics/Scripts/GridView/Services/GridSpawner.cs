using Tactics.Graphs.Services;
using Tactics.GridView.Data.Models;
using UnityEngine;
using Zenject;

namespace Tactics.GridView.Services
{
    public class GridSpawner : IInitializable
    {
        private readonly int _gridSize;
        private readonly float _tileSize;
        private readonly GameObject _tilePrefab;
        private readonly Transform _tilesRoot;
        
        private Grid2D<TileData> _grid2D;

        public GridSpawner(int gridSize, float tileSize, GameObject tilePrefab, Transform tilesRoot)
        {
            _gridSize = gridSize;
            _tileSize = tileSize;
            _tilePrefab = tilePrefab;
            _tilesRoot = tilesRoot;
        }

        public void Initialize()
        {
            _grid2D = new Grid2D<TileData>(_gridSize, _gridSize);
            SpawnTiles();
        }

        private void SpawnTiles()
        {
            var gridCenter = new Vector2(_gridSize / 2f, _gridSize / 2f);
            for (int i = 0; i < _grid2D.Graph.Nodes.Count; i++)
            {
                var tile = Object.Instantiate(_tilePrefab, _tilesRoot);
                var node = _grid2D.Graph.Nodes[i];
                node.Content.TileView = tile;
                var xPos = node.Content.XIndex * _tileSize - gridCenter.x;
                var zPos = node.Content.YIndex * _tileSize - gridCenter.y;
                tile.transform.position = new Vector3(xPos,tile.transform.position.y, zPos);
            }
        }
    }
}