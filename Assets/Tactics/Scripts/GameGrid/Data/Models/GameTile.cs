using Tactics.GameGrid.Implementation.Components;
using Tactics.GridView.Data.Models;

namespace Tactics.GameGrid.Data.Models
{
    public class GameTile : TileData
    {
        public GameTileComponent GameTileComponent { get; set; }
    }
}