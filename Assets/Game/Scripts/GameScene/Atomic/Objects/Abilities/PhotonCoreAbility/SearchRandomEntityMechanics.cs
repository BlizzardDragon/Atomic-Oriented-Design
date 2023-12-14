using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class SearchRandomEntityMechanics : IEnable, IDisable
    {
        private readonly AtomicEvent _searchRequest;

        private readonly AtomicVariable<Entity> _target;
        private readonly AtomicVariable<float> _serchRadius;
        private readonly AtomicVariable<Transform> _currentTransform;
        
        private readonly LayerMask _targetLayerMask;

        private readonly EntitySearchService _searchService = new();

        public SearchRandomEntityMechanics(
            AtomicEvent searchRequest,
            AtomicVariable<Entity> target,
            AtomicVariable<float> serchRadius,
            AtomicVariable<Transform> currentTransform,
            LayerMask targetLayerMask)
        {
            _searchRequest = searchRequest;
            _target = target;
            _serchRadius = serchRadius;
            _currentTransform = currentTransform;
            _targetLayerMask = targetLayerMask;
        }

        public void OnEnable() => _searchRequest.Subscribe(TryFindEntity);
        public void OnDisable() => _searchRequest.Unsubscribe(TryFindEntity);

        private void TryFindEntity()
        {
            Entity randomEntity;

            if (_searchService.TryFindRandomEntity(
                out randomEntity,
                _currentTransform.Value.position,
                _serchRadius,
                _targetLayerMask))
            {
                _target.Value = randomEntity;
            }
        }
    }
}