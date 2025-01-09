namespace Tactics.Graphs.Data.Models
{
    public class GridTile
    {
        public int XIndex { get; set; }
        public int YIndex { get; set; }
        
        public virtual bool IsOccupied()
        {
            return false;
        }
    }
}