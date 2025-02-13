using System;
using Tactics.GameGrid.Implementation.Services;
using Tactics.InputSystem.Services;
using Tactics.Raycasting.Data.Models;
using Tactics.Raycasting.Services;
using UniRx;
using UnityEngine.InputSystem;
using Zenject;

namespace Tactics.Players.Services
{
    public class PlayerInteractions : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _subscriptions = new();
        private readonly Subject<InteractionData> _playerInteractSubject = new();
        private readonly Subject<InteractionData> _tileInteractSubject = new();

        [Inject]
        private RayCaster _rayCaster;
        [Inject]
        private InputsProvider _inputsProvider;

        public IObservable<InteractionData> PlayerInteract => _playerInteractSubject.AsObservable();
        public IObservable<InteractionData> TileInteract => _tileInteractSubject.AsObservable();

        public void Initialize()
        {
            _inputsProvider.ToggleInput(true);
            _inputsProvider.OnInteract.Subscribe(OnPlayerInteract).AddTo(_subscriptions);
            _inputsProvider.OnInteract.Subscribe(OnTileInteract).AddTo(_subscriptions);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        private void OnPlayerInteract(InputAction.CallbackContext context)
        {
            if (!_rayCaster.TryCastRayFromCamera<InteractablePlayer>(1000, out var result, out var hitPoint, out var hitNormal))
            {
                return;
            }

            _playerInteractSubject.OnNext(new InteractionData(result, hitPoint, hitNormal));
        }

        private void OnTileInteract(InputAction.CallbackContext context)
        {
            if (!_rayCaster.TryCastRayFromCamera<InteractableTile>(1000, out var result, out var hitPoint, out var hitNormal))
            {
                return;
            }

            _tileInteractSubject.OnNext(new InteractionData(result, hitPoint, hitNormal));
        }
    }
}
