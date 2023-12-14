using System;
using Atomic;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class LifeSection
    {
        public AtomicVariable<bool> IsAlive = new();
        public AtomicVariable<int> HitPoints = new();
        public AtomicVariable<int> MaxHitPoints = new();

        public AtomicAction<int> TakeDamageRequest = new();
        public AtomicAction<int> TakeDamageAction = new();
        public AtomicEvent<int> TakeDamageEvent = new();

        public AtomicAction<int> HealingRequest = new();
        public AtomicEvent<int> HealingEvent = new();

        public AtomicEvent OnDeath = new();


        [Construct]
        public void Construct()
        {
            IsAlive.Value = true;

            TakeDamageRequest.Use(damage =>
            {
                if (IsAlive.Value)
                {
                    TakeDamageAction?.Invoke(damage);
                }
            });

            TakeDamageAction.Use(damage =>
            {
                HitPoints.Value -= damage;
                TakeDamageEvent?.Invoke(damage);
            });

            HealingRequest.Use(heal =>
            {
                if (!IsAlive) return;
                if (HitPoints.Value == MaxHitPoints) return;

                if (heal + HitPoints <= MaxHitPoints)
                {
                    HitPoints.Value += heal;
                    HealingEvent.Invoke(heal);
                }
                else
                {
                    var healValue = MaxHitPoints.Value - HitPoints.Value;
                    HitPoints.Value = MaxHitPoints;
                    HealingEvent.Invoke(healValue);
                }
            });

            HitPoints.Subscribe(hp =>
            {
                if (hp <= 0)
                {
                    OnDeath?.Invoke();
                    return;
                }
            });

            OnDeath.Subscribe(() => IsAlive.Value = false);
        }
    }
}