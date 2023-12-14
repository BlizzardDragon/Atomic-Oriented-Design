using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class MovementMechanics : IUpdate
    {
        private readonly AtomicVariable<bool> _canMove;
        private readonly AtomicVariable<float> _moveSpeed;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Transform _transform;

        public MovementMechanics(
            AtomicVariable<bool> canMove,
            AtomicVariable<float> moveSpeed,
            AtomicVariable<Vector3> moveDirection,
            Transform transform
        )
        {
            _canMove = canMove;
            _moveSpeed = moveSpeed;
            _moveDirection = moveDirection;
            _transform = transform;
        }

        public void Update(float deltaTime)
        {
            if (_canMove.Value)
            {
                _transform.position += _moveDirection.Value * _moveSpeed.Value * deltaTime;
            }
        }
    }
}