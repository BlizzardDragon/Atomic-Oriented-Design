using Atomic;
using UnityEngine;

public enum RotationType
{
    Slerp,
    RotateTowards,
}

namespace AtomicOrientedDesign.Shooter
{
    public class RotationEngine
    {
        private IAtomicValue<float> _rotationSpeedLerp;
        private IAtomicValue<float> _rotationSpeedTowards;
        private Transform _transform;


        public void Construct(
            Transform transform,
            IAtomicValue<float> rotationSpeedLerp,
            IAtomicValue<float> rotationSpeedTowards)
        {
            _transform = transform;
            _rotationSpeedLerp = rotationSpeedLerp;
            _rotationSpeedTowards = rotationSpeedTowards;
        }

        public void UpdateRotation(Vector3 targetDirection, RotationType rotationType)
        {
            if (targetDirection == Vector3.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            if (rotationType == RotationType.Slerp)
            {
                float step = _rotationSpeedLerp.Value * Time.deltaTime;
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, step);
            }
            else if (rotationType == RotationType.RotateTowards)
            {
                float step = _rotationSpeedTowards.Value * Time.deltaTime;
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, step);
            }
        }

        public void Rotate(Vector3 angle) => _transform.Rotate(angle);
    }
}