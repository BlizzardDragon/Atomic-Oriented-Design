using System;
using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class ResetTransformForEntityMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<Transform> _target;

        public ResetTransformForEntityMechanics(AtomicVariable<Transform> target)
        {
            _target = target;
        }


        public void OnEnable() => _target.Subscribe(OnTargetChanged);
        public void OnDisable() => _target.Unsubscribe(OnTargetChanged);

        private void OnTargetChanged(Transform transform)
        {
            if (_target.Value == null) return;

            var collider = _target.Value.GetComponent<Collider>();
            if (collider.attachedRigidbody)
            {
                var entity = collider.attachedRigidbody.GetComponent<Entity>();
                entity.Get<LifeSection>().OnDeath.Subscribe(ResetTransform);
            }
            else
            {
                throw new InvalidOperationException("Rigidbody is not attached!");
            }
        }

        private void ResetTransform() => _target.Value = null;
    }
}