using Tactics.Raycasting.Services;
using UnityEngine;

namespace Tactics.Raycasting.Sandbox.Services
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        public GameObject GameObject => gameObject;
    }
}
