using System;
using Tactics.InputSystem.Services;
using Tactics.Players.Data.Models;
using Tactics.Raycasting.Services;
using UniRx;
using UnityEngine.InputSystem;
using Zenject;

namespace Tactics.Players.Services
{
    public class PlayerInteractions : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _subscriptions = new();
        private readonly Subject<PlayerInteractionData> _interactSubject = new();
        
        [Inject]
        private RayCaster _rayCaster;
        [Inject]
        private InputsProvider _inputsProvider;
        
        public IObservable<PlayerInteractionData> OnPlayerInteract => _interactSubject.AsObservable();
        
        public void Initialize()
        {
            _inputsProvider.ToggleInput(true);
            _inputsProvider.OnInteract.Subscribe(OnInteract).AddTo(_subscriptions);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (!_rayCaster.TryCastRayFromCamera<InteractablePlayer>(1000, out var result, out var hitPoint, out var hitNormal))
            {
                return;
            }
            
            _interactSubject.OnNext(new PlayerInteractionData(result, hitPoint, hitNormal));
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}