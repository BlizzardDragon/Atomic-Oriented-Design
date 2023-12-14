using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IMoveComponent
    {
        bool MovementAllowed { get; set; }
        void Move(Vector3 direction);
    }

    public sealed class MoveComponent : IMoveComponent
    {
        private readonly IAtomicAction<Vector3> onMove;
        private readonly AtomicVariable<bool> _movementAllowed;

        public bool MovementAllowed { get => _movementAllowed.Value; set => _movementAllowed.Value = value; }

        public MoveComponent(IAtomicAction<Vector3> onMove, AtomicVariable<bool> movementAllowed)
        {
            this.onMove = onMove;
            _movementAllowed = movementAllowed;
        }

        public void Move(Vector3 direction)
        {
            this.onMove.Invoke(direction);
        }
    }
}