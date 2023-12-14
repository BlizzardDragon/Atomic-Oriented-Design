using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IMoveDirectionComponent
    {
        Vector3 MoveDirection { get; }
        void SetMoveDirection(Vector3 direction);
    }

    public sealed class MoveDirectionComponent : IMoveDirectionComponent
    {
        public Vector3 MoveDirection => _moveDirection.Value;
        private readonly AtomicVariable<Vector3> _moveDirection;

        public MoveDirectionComponent(AtomicVariable<Vector3> moveDirection)
        {
            _moveDirection = moveDirection;
        }

        public void SetMoveDirection(Vector3 direction) => _moveDirection.Value = direction;
    }
}