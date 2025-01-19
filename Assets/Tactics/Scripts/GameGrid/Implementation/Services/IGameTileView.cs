using UnityEngine;

namespace Tactics.GameGrid.Implementation.Services
{
    public interface IGameTileView
    {
        void ChangeColor(Color color);
        void ResetColor();
        void SetTileText(string text);
    }
}