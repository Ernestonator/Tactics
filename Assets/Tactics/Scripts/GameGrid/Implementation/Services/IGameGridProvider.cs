using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;
using Zenject;

namespace Tactics.GameGrid.Implementation.Services
{
    public interface IGameGridProvider
    {
        Grid2D<GameTile> Grid { get; }
    }
}