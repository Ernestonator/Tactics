using Tactics.HealthSystem.Data;
using Tactics.Units.Data.Models;
using UnityEngine;

namespace Tactics.Units.Services
{
    public class UnitDataContainer : MonoBehaviour
    {
        [field: SerializeField]
        public HealthParameters HealthParameters { get; private set; }
        [field: SerializeField]
        public MovementParameters MovementParameters { get; private set; }
        [field: SerializeField]
        public GameObject UnitGameObject { get; private set; }
    }
}
