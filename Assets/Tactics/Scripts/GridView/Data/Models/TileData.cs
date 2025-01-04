using Tactics.Graphs.Data.Models;

namespace Tactics.GridView.Data.Models
{
    public class TileData : GridTile
    {
        public ITileView TileView { get; set; }
    }
}