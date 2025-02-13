using Tactics.PlayerUnits.Services;
using Tactics.Units.Services;
using UnityEngine;

namespace Tactics.EnemyUnits.Implementation.Services
{
    public class InteractableEnemy : MonoBehaviour, IInteractableUnit
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
