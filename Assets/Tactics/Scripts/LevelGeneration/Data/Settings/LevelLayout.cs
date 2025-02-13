using System.Collections.Generic;
using Tactics.Common.Data.Models;
using Tactics.LevelGeneration.Data.Models;
using UnityEngine;

namespace Tactics.LevelGeneration.Data.Settings
{
    [CreateAssetMenu(menuName = GlobalConstants.ScriptableObjectFolder + nameof(LevelLayout),
        fileName = nameof(LevelLayout))]
    public class LevelLayout : ScriptableObject
    {
        [SerializeField]
        private List<UnitStartingPosition> _unitStartingPositions;

        internal IReadOnlyList<UnitStartingPosition> UnitStartingPositions => _unitStartingPositions;
    }
}
