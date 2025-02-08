using System;
using Tactics.Units.Services;

namespace Tactics.Players.Services
{
    public interface IPlayerInteractionBehavior
    {
        void DisplayActionRange(IUnitAction unitAction);
        void PerformAction(IUnitAction unitAction);
        void DisplayMovementRange(IUnitMovement unitMovement);
        void PerformMovement(IUnitMovement unitMovement);

        IObservable<InteractablePlayer> OnPlayerSelected { get; }
    }
}