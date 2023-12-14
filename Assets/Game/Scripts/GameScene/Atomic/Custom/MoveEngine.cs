using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class MoveEngine
    {
        private IAtomicValue<float> _speed;
        private Transform _transform;
        private Vector3 _direction;


        public void Construct(Transform transform, IAtomicValue<float> speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void Move(Vector3 direction)
        {
            _direction = direction.normalized;
            _transform.position += _direction * _speed.Value * Time.deltaTime;
        }
    }
}