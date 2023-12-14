using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class PeriodicAbilityModel : DeclarativeModel
    {
        // Data:
        [Header("Level")]
        public AtomicVariable<int> Level = new(1);

        [Header("Timer")]
        public AtomicEvent StopTimer;
        public AtomicEvent PlayTimer;
        public AtomicEvent ResetTimer;
        public AtomicEvent<float> SetCallbackTime;
        public AtomicEvent OnTimeIsOver;

        // Logic:
        protected CallbackTimer _callbackTimer;


        protected override void OnEnable()
        {
            base.OnEnable();
            Level.Subscribe(OnLevelUp);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Level.Unsubscribe(OnLevelUp);
        }

        [Construct]
        protected virtual void Construct()
        {
            _callbackTimer = new CallbackTimer(
                PlayTimer, StopTimer, ResetTimer, SetCallbackTime, OnTimeIsOver);
        }

        protected abstract void OnLevelUp(int level);
    }
}