using Tactics.Players.Services;
using UnityEngine;

namespace Tactics.Players.Data.Models
{
    public class PlayerInteractionData
    {
        public InteractablePlayer Player { get; }
        public Vector3 HitPoint { get; }
        public Vector3 HitNormal { get; }

        public PlayerInteractionData(InteractablePlayer player, Vector3 hitPoint, Vector3 hitNormal)
        {
            Player = player;
            HitPoint = hitPoint;
            HitNormal = hitNormal;
        }
    }
}