using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    interface IFireEventComponent
    {
        event Action<BulletArguments> FireEvent;
    }

    public class FireEventComponent : IFireEventComponent
    {
        private AtomicEvent<BulletArguments> _fireEvent;

        public event Action<BulletArguments> FireEvent
        {
            add => _fireEvent.Subscribe(value);
            remove => _fireEvent.Unsubscribe(value);
        }

        public FireEventComponent(AtomicEvent<BulletArguments> fireEvent)
        {
            _fireEvent = fireEvent;
        }
    }
}