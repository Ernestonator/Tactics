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
        
        [Inject]
        private PlayerInteractions _playerInteractions;
        [Inject]
        private GameGridHighlighter _gameGridHighlighter;
        
        public IObservable<InteractablePlayer> OnPlayerSelected => _playerSelectedSubject.AsObservable();
        
        public void Initialize()
        {
            _playerInteractions.OnPlayerInteract.Subscribe(OnPlayerInteract).AddTo(_subscriptions);
        }
        
        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
        
        public void DisplayActionRange(IUnitAction unitAction)
        {
            throw new System.NotImplementedException();
        }

        public void PerformAction(IUnitAction unitAction)
        {
            throw new System.NotImplementedException();
        }

        public void DisplayMovementRange(IUnitMovement unitMovement)
        {
            throw new System.NotImplementedException();
        }

        public void PerformMovement(IUnitMovement unitMovement)
        {
            throw new System.NotImplementedException();
        }

        private void OnPlayerInteract(InteractionData interactionData)
        {
            if (_currentlySelectedPlayer != (InteractablePlayer)interactionData.Interactable)
            {
                _currentlySelectedPlayer = (InteractablePlayer)interactionData.Interactable;
                _playerSelectedSubject.OnNext(_currentlySelectedPlayer);   
            }
            
            var tilesInRange = _currentlySelectedPlayer.UnitFacade.UnitMovement.GetTilesInRange().ToIndexes();
            // TODO configure colors
            _gameGridHighlighter.RequestGridHighlight(tilesInRange, Color.blue);
        }
    }
}