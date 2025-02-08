using Tactics.GameGrid.Data.Models;
using Tactics.GameGrid.Implementation.Services;
using Tactics.Graphs.Services;
using Tactics.LevelGeneration.Data.Settings;
using Tactics.PlayerUnits.Services;
using UnityEngine;
using Zenject;

namespace Tactics.LevelGeneration.Implementation.Services
{
    public class LevelLayoutSpawner : IInitializable
    {
        private readonly LevelLayout _levelLayout;
        
        [Inject]
        private UnitFactoriesMap _unitFactoriesMap;
        [Inject] 
        private IGameGridProvider _gameGridProvider;

        public LevelLayoutSpawner(LevelLayout levelLayout)
        {
            _levelLayout = levelLayout;
        }

        public void Initialize()
        {
            SpawnLevelLayout();
        }

        private void SpawnLevelLayout()
        {
            for (int i = 0; i < _levelLayout.UnitStartingPositions.Count; i++)
            {
                var unitStartingPosition = _levelLayout.UnitStartingPositions[i];
                if (_gameGridProvider.Grid.TryFindNodeAt(unitStartingPosition.UnitPosition, out var node) == false)
                {
                    Debug.LogError($"Could not find node at {unitStartingPosition.UnitPosition}. Can't spawn unit there.");
                    continue;
                }
                var factory = _unitFactoriesMap.GetFactory(unitStartingPosition.UnitType);
                var unit = factory.Create();
                SnapUnitToTile(unit, node);
            }
        }

        private void SnapUnitToTile(BaseUnitFacade unit, Node<GameTile> node)
        {
            unit.UnitMovement.TrySetLogicPosition(new Vector2Int(node.Content.XIndex, node.Content.YIndex));
            unit.UnitDataContainer.UnitGameObject.transform.SetParent(node.Content.TileView.TileGameObject.transform);
            unit.UnitDataContainer.UnitGameObject.transform.localPosition = Vector3.zero;
        }
    }
}