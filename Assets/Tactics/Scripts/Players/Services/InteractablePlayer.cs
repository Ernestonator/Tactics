using Tactics.PlayerUnits.Services;
using Tactics.Raycasting.Services;
using Tactics.Units.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Players.Services
{
    public class InteractablePlayer : MonoBehaviour, IInteractableUnit
    {
        [field: SerializeField]
        public GameObject GameObject { get; private set; }

        public BaseUnitFacade UnitFacade { get; private set; }
        
        public void SetUnitFacade(BaseUnitFacade unitFacade)
        {
            UnitFacade = unitFacade;
        }
    }
}