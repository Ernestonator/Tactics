using System;
using Tactics.Units.Services;
using UnityEngine;

namespace Tactics.Players.Services
{
    public interface IPlayerInteractionBehavior
    {
        IObservable<InteractablePlayer> OnPlayerSelected { get; }
        void DisplayActionRange(IUnitAction unitAction);
        void PerformAction(IUnitAction unitAction);
        void DisplayMovementRange(IUnitMovement unitMovement);
        void PerformMovement(IUnitMovement unitMovement, Vector2Int target);
    }
}
