using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IMagnetComponent
    {
        void SetRadius(float radius);
        void IncreaseRadius(float extraRadius);
        void TryAddTarget(Transform target);
    }

    public sealed class MagnetComponent : IMagnetComponent
    {

        private readonly AtomicVariable<float> _radius;
        private readonly AtomicEvent<Transform> _onTargetEnter;

        public MagnetComponent(AtomicVariable<float> radius, AtomicEvent<Transform> onTargetEnter)
        {
            _radius = radius;
            _onTargetEnter = onTargetEnter;
        }


        public void SetRadius(float radius) => _radius.Value = radius;
        public void IncreaseRadius(float extraRadius) => _radius.Value += extraRadius;
        public void TryAddTarget(Transform target) => _onTargetEnter?.Invoke(target);
    }
}