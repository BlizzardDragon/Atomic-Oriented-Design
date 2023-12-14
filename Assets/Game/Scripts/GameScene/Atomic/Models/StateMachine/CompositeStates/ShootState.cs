using System;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class ShootState : CompositeState
    {
        public SearchTargetState SearchTarget;


        [Construct]
        public void ConstructSelf()
        {
            SetStates(SearchTarget);
        }

        [Construct]
        public void ConstructSubStates(PlayerModel_View view, TargetSearchSection searchSection)
        {
            SearchTarget.Construct(searchSection);
        }
    }
}