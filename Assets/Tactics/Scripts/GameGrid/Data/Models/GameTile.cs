using Tactics.GameGrid.Implementation.Services;
using Tactics.GridView.Data.Models;

namespace Tactics.GameGrid.Data.Models
{
    public class GameTile : TileData
    {
        public IGameTileView GameTileView { get; set; }
        private bool Occupied { get; set; }

        public override bool IsOccupied()
        {
            return Occupied;
        }

        public void SetOccupied(bool occupied)
        {
            Occupied = occupied;
        }
    }
}
