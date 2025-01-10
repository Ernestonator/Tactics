using Tactics.Raycasting.Services;
using UnityEngine;

namespace Tactics.Players.Services
{
    public class InteractablePlayer : MonoBehaviour, IInteractable
    {
        [field: SerializeField]
        public GameObject GameObject { get; private set; }
    }
}