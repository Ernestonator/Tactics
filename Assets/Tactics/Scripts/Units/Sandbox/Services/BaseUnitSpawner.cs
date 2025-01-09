using System.Collections.Generic;
using JetBrains.Annotations;
using Tactics.GameGrid.Implementation.Services;
using Tactics.PlayerUnits.Services;
using Tactics.Units.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Sandbox.Services
{
    public class BaseUnitSpawner : MonoBehaviour
    {
        private readonly Stack<BaseUnitFacade> _units = new();
        
        [SerializeField] private Vector2Int spawnPoint;
        
        [CanBeNull] private BaseUnitFacade _activeUnit;
        
        [Inject]
        BaseUnitFactory _factory;
        [Inject]
        GameGridSpawner _gameGridSpawner;

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            _gameGridSpawner.Grid.TryFindNodeAt(spawnPoint.x, spawnPoint.y, out var node);
            if (node.Content.IsOccupied())
            {
                Debug.LogError($"Cannot spawn unit at {spawnPoint.x}, {spawnPoint.y}");
                return;
            }
            
            var unit = _factory.Create();
            _units.Push(unit);
            unit.UnitDataContainer.UnitGameObject.transform.SetParent(node.Content.TileView.TileGameObject.transform);
            unit.UnitDataContainer.UnitGameObject.transform.localPosition = Vector3.zero;
            unit.UnitMovement.TrySetLogicPosition(new Vector2Int(node.Content.XIndex, node.Content.YIndex));
        }

        [ContextMenu(nameof(DespawnLast))]
        public void DespawnLast()
        {
            var lastUnit = _units.Pop();
            lastUnit.Dispose();
        }
        
        [ContextMenu(nameof(SeeRangeOfLatestUnit))]
        public void SeeRangeOfLatestUnit()
        {
            var lastUnit = _units.Peek();
            _activeUnit = lastUnit;
            var nodes = lastUnit.UnitMovement.GetTilesInRange();

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Content.GameTileComponent.ChangeColor(Color.red);
            }
        }
        
        [ContextMenu(nameof(ResetRangeOfLatestUnit))]
        public void ResetRangeOfLatestUnit()
        {
            if (_activeUnit == null)
            {
                return;
            }
            
            var nodes = _activeUnit.UnitMovement.GetTilesInRange();

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Content.GameTileComponent.ResetColor();
            }
        }
    }
}