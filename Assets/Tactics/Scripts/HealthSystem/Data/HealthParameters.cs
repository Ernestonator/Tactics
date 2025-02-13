using System;
using UnityEngine;

namespace Tactics.HealthSystem.Data
{
    [Serializable]
    public class HealthParameters
    {
        [field: SerializeField]
        public int StartingHealth { get; private set; }
    }
}
