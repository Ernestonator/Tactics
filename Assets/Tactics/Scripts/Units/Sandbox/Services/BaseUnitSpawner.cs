using System.Collections.Generic;
using JetBrains.Annotations;
using Tactics.GameGrid.Data.Models;
using Tactics.GameGrid.Implementation.Services;
using Tactics.Graphs.Services;
using Tactics.PlayerUnits.Services;
using Tactics.Units.Data;
using Tactics.Units.Services;
using UnityEngine;
using Zenject;

namespace Tactics.Units.Sandbox.Services
{
    public class BaseUnitSpawner : MonoBehaviour
    {
        private readonly Stack<BaseUnitFacade> _units = new();
        
        [SerializeField] private Vector2Int spawnPoint;
        [SerializeField] private Vector2Int targetPoint;
        
        [CanBeNull] private BaseUnitFacade _activeUnit;
        
        [Inject(Id = UnitConstants.PlayerUnitFactoryID)]
        private BaseUnitFactory _factory;
        [Inject]
        private GameGridSpawner _gameGridSpawner;

        private List<Node<GameTile>> _activePath;
        
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
            unit.UnitMovement.TrySetLogicPosition(new Vector2Int(node.Content.XIndex, node.Content.YIndex));
            unit.UnitDataContainer.UnitGameObject.transform.SetParent(node.Content.TileView.TileGameObject.transform);
            unit.UnitDataContainer.UnitGameObject.transform.localPosition = Vector3.zero;
            _activeUnit = unit;
        }

        [ContextMenu(nameof(DespawnLast))]
        public void DespawnLast()
        {
            var lastUnit = _units.Pop();
            lastUnit.Dispose();
            _activeUnit = _units.Peek();
        }
        
        [ContextMenu(nameof(SeeRangeOfLatestUnit))]
        public void SeeRangeOfLatestUnit()
        {
            var lastUnit = _units.Peek();
            var nodes = lastUnit.UnitMovement.GetTilesInRange();

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Content.GameTileView.ChangeColor(Color.red);
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
                nodes[i].Content.GameTileView.ResetColor();
            }
        }
        
        [ContextMenu(nameof(CalculatePathForLatestUnit))]
        public void CalculatePathForLatestUnit()
        {
            if (_activeUnit == null)
            {
                return;
            }

            ResetPathForLatestUnit();
            var nodes = _activeUnit.UnitMovement.CalculatePath(targetPoint);

            if (nodes == null)
            {
                Debug.LogError($"Can't reach path to {targetPoint}.");
                return;
            }

            _activePath = nodes;

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Content.GameTileView.ChangeColor(Color.green);
            }
        }
        
        [ContextMenu(nameof(ResetPathForLatestUnit))]
        public void ResetPathForLatestUnit()
        {
            if (_activePath == null)
            {
                return;
            }
            
            for (int i = 0; i < _activePath.Count; i++)
            {
                _activePath[i].Content.GameTileView.ResetColor();
            }
        }
        
        [ContextMenu(nameof(PerformMovementToLatestPath))]
        public void PerformMovementToLatestPath()
        {
            if (_activeUnit == null)
            {
                return;
            }
            
            if (_activePath == null)
            {
                return;
            }

            ResetRangeOfLatestUnit();
            ResetPathForLatestUnit();

            var lastNode = _activePath[^1];
            _activeUnit.UnitMovement.TrySetLogicPosition(new Vector2Int(lastNode.Content.XIndex, lastNode.Content.YIndex));
            _activeUnit.UnitDataContainer.UnitGameObject.transform.SetParent(lastNode.Content.TileView.TileGameObject.transform);
            _activeUnit.UnitDataContainer.UnitGameObject.transform.localPosition = Vector3.zero;
        }
    }
}