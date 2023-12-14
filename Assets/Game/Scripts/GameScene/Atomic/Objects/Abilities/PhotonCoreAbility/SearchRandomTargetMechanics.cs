using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class SearchRandomTargetMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<Transform> _target;
        private readonly AtomicEvent _searchEvent;

        private readonly Transform _currentTransform;
        private readonly float _serchRadius;
        private readonly LayerMask _targetLayerMask;

        private readonly TargetSearchService _searchService = new();

        public SearchRandomTargetMechanics(
            AtomicVariable<Transform> target,
            Transform currentTransform,
            float serchRadius,
            LayerMask targetLayerMask,
            AtomicEvent searchEvent)
        {
            _target = target;
            _currentTransform = currentTransform;
            _serchRadius = serchRadius;
            _targetLayerMask = targetLayerMask;
            _searchEvent = searchEvent;
        }

        public void OnEnable() => _searchEvent.Subscribe(TryFindTarget);
        public void OnDisable() => _searchEvent.Unsubscribe(TryFindTarget);

        private void TryFindTarget()
        {
            if (_target.Value == null)
            {
                Transform randomTarget;

                if (_searchService.TryFindRandomTarget(
                    out randomTarget,
                    _currentTransform.position,
                    _serchRadius,
                    _targetLayerMask))
                {
                    _target.Value = randomTarget;
                }
            }
        }
    }
}