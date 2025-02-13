using System.Collections.Generic;
using Tactics.GameGrid.Data.Models;
using Tactics.Graphs.Services;

namespace Tactics.Units.Services
{
    public interface IUnitAction
    {
        List<Node<GameTile>> GetActionRange();
        void PerformAction();
    }
}
