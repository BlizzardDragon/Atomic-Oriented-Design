using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IRotationComponent
    {
        void RotationRequest(Vector3 direction);
    }

    public class RotationComponent : IRotationComponent
    {
        private AtomicEvent<Vector3> _rotationRequest;

        public RotationComponent(AtomicEvent<Vector3> rotationRequest)
        {
            _rotationRequest = rotationRequest;
        }

        public void RotationRequest(Vector3 direction)
        {
            _rotationRequest?.Invoke(direction);
        }
    }
}