using System.Collections.Generic;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class MagnetMechanics : IUpdate
    {
        private Transform _currentTransform;
        private AtomicVariable<float> _speed;
        private readonly List<Transform> _targets = new();


        public void Construct(Transform currentTransform, AtomicVariable<float> speed)
        {
            _currentTransform = currentTransform;
            _speed = speed;
        }

        public void Update(float deltaTime)
        {
            foreach (var target in _targets)
            {
                var direction = _currentTransform.position - target.position;
                var normalizedDirection = direction.normalized;
                target.Translate(normalizedDirection * _speed * deltaTime, Space.World);
            }
        }

        public void TryAddTarget(Transform target)
        {
            if (!_targets.Contains(target))
            {
                _targets.Add(target);
            }
        }

        public void TryRemoveTarget(Transform target)
        {
            if(_targets.Contains(target))
            {
                _targets.Remove(target);
            }
        }
    }
}