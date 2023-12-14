using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class RotationAroundMechanics : IStart, IUpdate
    {
        private readonly AtomicVariable<Transform> _currentTransform;
        private readonly AtomicVariable<Transform> _rotationCenter;
        private readonly AtomicVariable<float> _rotationSpeed;
        private readonly AtomicVariable<float> _rotationRadius;

        public RotationAroundMechanics(
            AtomicVariable<Transform> currentTransform,
            AtomicVariable<Transform> rotationCenter,
            AtomicVariable<float> rotationSpeed,
            AtomicVariable<float> rotationRadius)
        {
            _currentTransform = currentTransform;
            _rotationCenter = rotationCenter;
            _rotationSpeed = rotationSpeed;
            _rotationRadius = rotationRadius;
        }

        public void Start()
        {
            _currentTransform.Value.transform.SetParent(null);
        }

        public void Update(float deltaTime)
        {
            var targetPos = _rotationCenter.Value.position;

            _currentTransform.Value.position = new Vector3(
                targetPos.x + Mathf.Sin(_rotationSpeed * Time.time) * _rotationRadius.Value, 
                _currentTransform.Value.position.y, 
                targetPos.z + Mathf.Cos(_rotationSpeed * Time.time) * _rotationRadius.Value);
        }
    }
}