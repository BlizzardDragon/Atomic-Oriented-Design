using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IHitPointsComponent
    {
        int HitPoints { get; }
        int MaxHitPoints { get; }
        event Action<int> OnHitPointsChanged;
        void HealingRequest(int heal);
        void SetHitPoints(int points);
        void SetMaxHitPoints(int maxPoints);
        void IncreaseMaxHealth(int extraHealth);
    }

    public sealed class HitPointsComponent : IHitPointsComponent
    {
        public int HitPoints => _hitPoints.Value;
        public int MaxHitPoints => _maxHitPoints.Value;

        public event Action<int> OnHitPointsChanged
        {
            add => _hitPoints.Subscribe(value);
            remove => _hitPoints.Unsubscribe(value);
        }

        private readonly AtomicVariable<int> _hitPoints;
        private readonly AtomicVariable<int> _maxHitPoints;
        private readonly AtomicAction<int> _healingRequest;

        public HitPointsComponent(
            AtomicVariable<int> hitPoints,
            AtomicVariable<int> maxHitPoints,
            AtomicAction<int> healingRequest)
        {
            _hitPoints = hitPoints;
            _maxHitPoints = maxHitPoints;
            _healingRequest = healingRequest;
        }

        public void HealingRequest(int heal) => _healingRequest?.Invoke(heal);
        public void SetHitPoints(int points) => _hitPoints.Value = points;
        public void SetMaxHitPoints(int maxPoints) => _maxHitPoints.Value = maxPoints;
        public void IncreaseMaxHealth(int extraHealth) => _maxHitPoints.Value += extraHealth;
    }
}