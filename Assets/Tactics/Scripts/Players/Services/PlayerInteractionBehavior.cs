using System;
using Tactics.Raycasting.Data.Models;
using Tactics.Units.Services;
using UniRx;
using Zenject;

namespace Tactics.Players.Services
{
    public class PlayerInteractionBehavior : IPlayerInteractionBehavior, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
        private readonly Subject<InteractablePlayer> _playerSelectedSubject = new();
        
        private InteractablePlayer _currentlySelectedPlayer;
        
        [Inject]
        private PlayerInteractions _playerInteractions;
        
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
            
            // TODO use facade for movement
            // _currentlySelectedPlayer.UnitFacade
        }
    }
}