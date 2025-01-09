using System;
using UnityEngine;

namespace Tactics.Units.Data.Models
{
    [Serializable]
    public class MovementParameters
    {
        [field: SerializeField]
        public Vector2Int StartingPosition { get; private set; }
        [field: SerializeField]
        public int Range { get; private set; }
    }
}