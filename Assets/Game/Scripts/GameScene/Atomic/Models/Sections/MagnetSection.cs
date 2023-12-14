using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class MagnetSection
    {
        public AtomicVariable<float> Radius = new();
        public AtomicVariable<float> Speed = new();

        public AtomicEvent<Transform> OnTargetEnter = new();
        public AtomicEvent<Transform> OnTargetDestroyed = new();

        public SphereCollider MagnetCollider;
        private MagnetMechanics _magnetMechanics = new();


        [Construct]
        public void Construct(TransformSection transform)
        {
            _magnetMechanics.Construct(transform.Transform, Speed);

            Radius.Subscribe(radius => MagnetCollider.radius = radius);
            OnTargetEnter.Subscribe(target => _magnetMechanics.TryAddTarget(target));
            OnTargetDestroyed.Subscribe(target => _magnetMechanics.TryRemoveTarget(target));
        }
    }
}