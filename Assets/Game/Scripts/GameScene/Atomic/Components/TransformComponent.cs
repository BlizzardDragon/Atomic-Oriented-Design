using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface ITransformComponent
    {
        Transform EntityTransform { get; }
    }

    public class TransformComponent : ITransformComponent
    {
        public Transform EntityTransform => _entityTransform;
        private Transform _entityTransform;

        public TransformComponent(Transform entityTransform)
        {
            _entityTransform = entityTransform;
        }
    }
}