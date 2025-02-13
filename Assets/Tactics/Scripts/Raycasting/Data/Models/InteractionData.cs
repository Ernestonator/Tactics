using Tactics.Raycasting.Services;
using UnityEngine;

namespace Tactics.Raycasting.Data.Models
{
    public class InteractionData
    {
        public IInteractable Interactable { get; }
        public Vector3 HitPoint { get; }
        public Vector3 HitNormal { get; }

        public InteractionData(IInteractable interactable, Vector3 hitPoint, Vector3 hitNormal)
        {
            Interactable = interactable;
            HitPoint = hitPoint;
            HitNormal = hitNormal;
        }
    }
}
