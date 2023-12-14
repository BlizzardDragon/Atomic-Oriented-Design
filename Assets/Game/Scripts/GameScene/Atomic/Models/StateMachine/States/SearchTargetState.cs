using System;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class SearchTargetState : UpdateState
    {
        private TargetSearchSection _searchSection;


        public void Construct(TargetSearchSection searchSection)
        {
            _searchSection = searchSection;
        }
        
        protected override void OnUpdate(float deltaTime)
        {
            _searchSection.SearchRequest?.Invoke();
        }
    }
}