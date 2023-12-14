using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class TargetSearchSection
    {
        public AtomicVariable<float> AttackRadius = new();
        public AtomicVariable<int> TargetLayerMask = new();
        public AtomicVariable<bool> TargetIsNotFound = new();

        public AtomicEvent SearchRequest = new();
        public AtomicEvent<Transform> TargetFound = new();
        public AtomicEvent TargetNotFound = new();

        private TargetSearchService _findNearestTarget = new();

        Transform _currentTransform;


        public void SetTransform(Transform currentTransform) => _currentTransform = currentTransform;

        [Construct]
        public void Construct()
        {
            SearchRequest.Subscribe(() =>
            {
                if (_findNearestTarget.TryFindNearestTarget(
                        out var target,
                        _currentTransform.position,
                        AttackRadius.Value,
                        TargetLayerMask.Value))
                {
                    TargetFound?.Invoke(target);
                    TargetIsNotFound.Value = false;
                }
                else
                {
                    TargetNotFound?.Invoke();
                    TargetIsNotFound.Value = true;
                }
            });
        }
    }
}