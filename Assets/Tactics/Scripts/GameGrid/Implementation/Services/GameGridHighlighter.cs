using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Tactics.GameGrid.Implementation.Services
{
    public class GameGridHighlighter
    {
        private List<Vector2Int> _highlightedTiles = new();

        [Inject]
        private IGameGridProvider _gameGridProvider;

        public void RequestGridHighlight(List<Vector2Int> tilesToHighlight, Color targetColor)
        {
            RequestGridHighlightReset();

            _highlightedTiles = tilesToHighlight;

            for (var i = 0; i < _highlightedTiles.Count; i++)
            {
                var tile = _highlightedTiles[i];

                if (_gameGridProvider.Grid.TryFindNodeAt(tile.x, tile.y, out var node) == false)
                {
                    Debug.LogError($"Could not find node at {tile.x}, {tile.y}. Can't highlight tiles.");
                }

                node.Content.GameTileView.ChangeColor(targetColor);
            }
        }

        public void RequestGridHighlightReset()
        {
            for (var i = 0; i < _highlightedTiles.Count; i++)
            {
                var tile = _highlightedTiles[i];

                if (_gameGridProvider.Grid.TryFindNodeAt(tile.x, tile.y, out var node) == false)
                {
                    Debug.LogError($"Could not find node at {tile.x}, {tile.y}. Can't reset all highlighted tiles.");
                }

                node.Content.GameTileView.ResetColor();
            }
        }
    }
}
