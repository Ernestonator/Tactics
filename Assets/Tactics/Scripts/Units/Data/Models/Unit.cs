using System;
using Tactics.Units.Services;
using UnityEngine;

namespace Tactics.Units.Data.Models
{
    [Serializable]
    public class Unit
    {
        [field: SerializeField]
        public UnitDataContainer DataContainer { get; private set; }
    }
}