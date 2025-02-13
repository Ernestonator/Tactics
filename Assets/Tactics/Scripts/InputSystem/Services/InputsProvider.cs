using System;
using UniRx;
using UnityEngine.InputSystem;
using Zenject;

namespace Tactics.InputSystem.Services
{
    public class InputsProvider : IInitializable, IDisposable
    {
        private readonly Subject<InputAction.CallbackContext> _interactSubject = new();

        [Inject]
        private InputSystem_Actions _inputSystem;

        public IObservable<InputAction.CallbackContext> OnInteract => _interactSubject.AsObservable();

        public void Initialize()
        {
            _inputSystem.Player.Interact.performed += _interactSubject.OnNext;
        }

        public void Dispose()
        {
            _inputSystem.Player.Interact.performed -= _interactSubject.OnNext;

            _inputSystem?.Dispose();
            _interactSubject?.Dispose();
        }

        public void ToggleInput(bool isOn)
        {
            if (isOn)
            {
                _inputSystem.Enable();
            }
            else
            {
                _inputSystem.Disable();
            }
        }
    }
}
