using System;
using Tactics.GameGrid.Implementation.Extensions;
using Tactics.GameGrid.Implementation.Services;
using Tactics.Raycasting.Data.Models;
using Tactics.Units.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tactics.Players.Services
{
    public class PlayerInteractionBehavior : IPlayerInteractionBehavior, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _subscriptions = new();
        private readonly Subject<InteractablePlayer> _playerSelectedSubject = new();

        private InteractablePlayer _currentlySelectedPlayer;
        private InteractableTile _currentlySelectedTile;

        [Inject]
        private PlayerInteractions _playerInteractions;
        [Inject]
        private GameGridHighlighter _gameGridHighlighter;

        public IObservable<InteractablePlayer> OnPlayerSelected => _playerSelectedSubject.AsObservable();

        public void Initialize()
        {
            _playerInteractions.PlayerInteract.Subscribe(OnPlayerInteract).AddTo(_subscriptions);
            _playerInteractions.TileInteract.Subscribe(OnTileInteract).AddTo(_subscriptions);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        public void DisplayActionRange(IUnitAction unitAction)
        {
            throw new NotImplementedException();
        }

        public void PerformAction(IUnitAction unitAction)
        {
            throw new NotImplementedException();
        }

        public void DisplayMovementRange(IUnitMovement unitMovement)
        {
            var tilesInRange = unitMovement.GetTilesInRange().ToIndexes();
            // TODO configure colors
            _gameGridHighlighter.RequestGridHighlight(tilesInRange, Color.blue);
        }

        public void PerformMovement(IUnitMovement unitMovement, Vector2Int target)
        {
            unitMovement.TrySetLogicPosition(target);
            var unitGameObject = _currentlySelectedPlayer.UnitFacade.UnitDataContainer.UnitGameObject;
            unitGameObject.transform.SetParent(_currentlySelectedTile.Node.Content.TileView.TileGameObject.transform);
            unitGameObject.transform.localPosition = Vector3.zero;
            _gameGridHighlighter.RequestGridHighlightReset();
        }

        private void OnPlayerInteract(InteractionData interactionData)
        {
            if (_currentlySelectedPlayer != (InteractablePlayer)interactionData.Interactable)
            {
                _currentlySelectedPlayer = (InteractablePlayer)interactionData.Interactable;
                _playerSelectedSubject.OnNext(_currentlySelectedPlayer);
            }

            DisplayMovementRange(_currentlySelectedPlayer.UnitFacade.UnitMovement);
        }

        private void OnTileInteract(InteractionData interactionData)
        {
            if (_currentlySelectedPlayer == null)
            {
                return;
            }

            var tileInteraction = (InteractableTile)interactionData.Interactable;
            _currentlySelectedTile = tileInteraction;
            var unitMovement = _currentlySelectedPlayer.UnitFacade.UnitMovement;
            var unitLastPosition = unitMovement.LastPosition;
            var nodeContent = tileInteraction.Node.Content;
            var tilePosition = new Vector2Int(nodeContent.XIndex, nodeContent.YIndex);

            if (unitLastPosition == tilePosition)
            {
                Debug.LogWarning("Trying to move player to the same tile");
                return;
            }

            if (unitMovement.IsTileReachable(tilePosition) == false)
            {
                Debug.LogWarning("Trying to move player to unreachable tile.");
                return;
            }

            PerformMovement(unitMovement, tilePosition);
        }
    }
}
