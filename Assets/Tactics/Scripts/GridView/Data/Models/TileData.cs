using Tactics.Graphs.Data.Models;

namespace Tactics.GridView.Data.Models
{
    public class TileData : GridTile, ITileData
    {
        public ITileView TileView { get; set; }
    }
}
