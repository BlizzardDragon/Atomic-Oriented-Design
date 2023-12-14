using System;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class PlayerIdleState : CompositeState
    {
        public SearchTargetState SearchTarget;


        [Construct]
        public void ConstructSelf()
        {
            SetStates(SearchTarget);
        }

        [Construct]
        public void ConstructSubStates(TargetSearchSection searchSection)
        {
            SearchTarget.Construct(searchSection);
        }
    }
}