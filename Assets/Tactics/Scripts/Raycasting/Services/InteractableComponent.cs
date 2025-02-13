using UnityEngine;

namespace Tactics.Raycasting.Services
{
    public class InteractableComponent : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public GameObject GameObject { get; private set; }
    }
}
