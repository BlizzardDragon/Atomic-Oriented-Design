using System;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class EnemyIdleViewState : CompositeState
    {
        public TakeDamageState TakeDamage = new();


        [Construct]
        public void ConstructSelf()
        {
            SetStates(TakeDamage);
        }

        [Construct]
        public void ConstructSubStates(Animator animator, LifeSection life)
        {
            TakeDamage.Construct(animator, life.TakeDamageEvent);
        }
    }
}