using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class AbilityModel : DeclarativeModel
    {
        [Header("Level")]
        public AtomicVariable<int> Level = new(1);

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

        protected abstract void OnLevelUp(int level);
    }
}