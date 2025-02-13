using System;
using Tactics.Raycasting.Data.Models;
using Tactics.Raycasting.Services;
using UniRx;
using Zenject;

namespace Tactics.GameGrid.Implementation.Services
{
    public class GridInteractions : ITickable, IDisposable
    {
        private readonly CompositeDisposable _subscriptions = new();
        private readonly Subject<InteractionData> _hoverStartSubject = new();
        private readonly Subject<InteractionData> _hoverEndSubject = new();

        private InteractionData _currentInteraction;

        [Inject]
        private RayCaster _rayCaster;

        public IObservable<InteractionData> OnTileHoverStart => _hoverStartSubject.AsObservable();
        public IObservable<InteractionData> OnTileHoverEnd => _hoverEndSubject.AsObservable();

        public void Tick()
        {
            var raycastHit = _rayCaster.TryCastRayFromCamera<InteractableTile>(1000, out var result, out var hitPoint, out var hitNormal);

            if (raycastHit == false)
            {
                if (_currentInteraction != null)
                {
                    _hoverEndSubject.OnNext(_currentInteraction);
                    _currentInteraction = null;
                }

                return;
            }

            if (_currentInteraction != null)
            {
                if (result.GameObject.GetInstanceID() == _currentInteraction.Interactable.GameObject.GetInstanceID())
                {
                    return;
                }

                _hoverEndSubject.OnNext(_currentInteraction);
            }

            _currentInteraction = new InteractionData(result, hitPoint, hitNormal);
            _hoverStartSubject.OnNext(_currentInteraction);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
            _hoverStartSubject?.Dispose();
            _hoverEndSubject?.Dispose();
        }
    }
}
