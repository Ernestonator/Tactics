using Tactics.InputSystem.Services;
using Tactics.Raycasting.Services;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Tactics.Raycasting.Sandbox.Services
{
    public class TestInteractionService : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private TestInteractable testInteractablePrefab;

        [Inject]
        private InputsProvider _inputsProvider;

        [Inject]
        private RayCaster _rayCaster;

        public void Initialize()
        {
            _inputsProvider.ToggleInput(true);
            _inputsProvider.OnInteract.Subscribe(OnInteract).AddTo(this);
        }

        [ContextMenu(nameof(SpawnInteractableObject))]
        private void SpawnInteractableObject()
        {
            Instantiate(testInteractablePrefab);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (!_rayCaster.TryCastRayFromCamera<TestInteractable>(1000, out var result, out var hitPoint, out _))
            {
                return;
            }

            Debug.Log($"Hit: {result.GameObject.name} at {hitPoint}");
        }
    }
}
