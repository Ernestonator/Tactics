using System;
using Tactics.HealthSystem.Data;
using UniRx;
using UnityEngine;

namespace Tactics.HealthSystem.Implementation.Services
{
    public class HealthTarget : IHealthTarget
    {
        private readonly Subject<Unit> _targetDiedSubject = new();

        public IObservable<Unit> TargetDied => _targetDiedSubject;
        public int StartingHealth { get; }
        public ReactiveProperty<int> CurrentHealth { get; } = new();
        public bool IsDead => CurrentHealth.Value <= 0;

        public HealthTarget(HealthParameters healthParameters)
        {
            StartingHealth = healthParameters.StartingHealth;
            CurrentHealth.Value = healthParameters.StartingHealth;
        }

        public void TakeHit(int damage)
        {
            UpdateHealthPoints(-damage);
        }

        public void Heal(int healPoints)
        {
            UpdateHealthPoints(healPoints);
        }

        private void UpdateHealthPoints(int pointsToAdd)
        {
            var newHealth = Mathf.Clamp(CurrentHealth.Value + pointsToAdd, 0, StartingHealth);
            CurrentHealth.Value = newHealth;

            if (CurrentHealth.Value == 0)
            {
                _targetDiedSubject.OnNext(Unit.Default);
            }
        }
    }
}
