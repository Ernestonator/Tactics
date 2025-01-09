using Tactics.GameGrid.Implementation.Components;
using Tactics.GridView.Data.Models;

namespace Tactics.GameGrid.Data.Models
{
    public class GameTile : TileData
    {
        private bool Occupied { get; set; } = false;
        public GameTileComponent GameTileComponent { get; set; }

        public void SetOccupied(bool occupied)
        {
            Occupied = occupied;
        }

        public override bool IsOccupied()
        {
            return Occupied;
        }
    }
}