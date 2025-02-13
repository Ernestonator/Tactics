using System;
using UniRx;

namespace Tactics.HealthSystem.Implementation.Services
{
    public interface IHealthTarget
    {
        IObservable<Unit> TargetDied { get; }
        int StartingHealth { get; }
        ReactiveProperty<int> CurrentHealth { get; }
        bool IsDead { get; }

        void TakeHit(int damage);
        void Heal(int healPoints);
    }
}
