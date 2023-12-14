using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IMovementDirectionComponent
    {
        bool MovementAllowed { get; set; }
        void SetDirection(Vector3 direction);
    }

    public class MovementDirectionComponent : IMovementDirectionComponent
    {
        private MovementDirectionVariable _directionVariable;
        private readonly AtomicVariable<bool> _movementAllowed;

        public bool MovementAllowed { get => _movementAllowed.Value; set => _movementAllowed.Value = value; }

        public MovementDirectionComponent(
            MovementDirectionVariable directionVariable,
            AtomicVariable<bool> movementAllowed)
        {
            _directionVariable = directionVariable;
            _movementAllowed = movementAllowed;
        }

        public void SetDirection(Vector3 direction)
        {
            _directionVariable.Value = direction;
        }
    }
}