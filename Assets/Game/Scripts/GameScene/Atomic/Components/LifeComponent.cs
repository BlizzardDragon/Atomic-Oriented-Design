using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface ILifeComponent
    {
        bool IsAlive { get; }
        event Action OnDeath;
    }

    public class LifeComponent : ILifeComponent
    {
        public bool IsAlive => _isAlive.Value;
        private AtomicVariable<bool> _isAlive;
        private AtomicEvent _onDeath;

        public event Action OnDeath
        {
            add => _onDeath.Subscribe(value);
            remove => _onDeath.Unsubscribe(value);
        }

        public LifeComponent(AtomicVariable<bool> isAlive, AtomicEvent onDeath)
        {
            _isAlive = isAlive;
            _onDeath = onDeath;
        }
    }
}