using Tactics.PlayerUnits.Services;
using Tactics.Raycasting.Services;

namespace Tactics.Units.Services
{
    public interface IInteractableUnit : IInteractable
    {
        BaseUnitFacade UnitFacade { get; }
        void SetUnitFacade(BaseUnitFacade unitFacade);
    }
}
