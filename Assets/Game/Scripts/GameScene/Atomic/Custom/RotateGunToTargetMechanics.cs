using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class RotateGunToTargetMechanics
    {
        public void Construct(
        Transform gun,
        Transform currentTransform,
        AtomicEvent<Transform> targetFound,
        AtomicVariable<float> thresholdFireAngle,
        AtomicEvent targetOutAim,
        AtomicEvent targetInAim,
        RotationEngine rotationEngine,
        RotationType rotationType)
        {
            targetFound.Subscribe(target =>
            {
                Vector3 directionPlayerToTarget = target.position - currentTransform.position;
                directionPlayerToTarget.y = 0;

                rotationEngine.UpdateRotation(directionPlayerToTarget, rotationType);

                if (Vector3.Angle(directionPlayerToTarget, gun.forward) < thresholdFireAngle.Value)
                {
                    targetInAim?.Invoke();
                }
                else
                {
                    targetOutAim?.Invoke();
                }
            });
        }
    }
}