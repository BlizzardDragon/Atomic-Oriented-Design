using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class RotateState : IState
    {
        [SerializeField] private RotationType _rotationType = RotationType.Slerp;

        private MovementDirectionVariable _directionVariable;
        private RotationEngine _rotationEngine;


        public void Construct(MovementDirectionVariable directionVariable, RotationEngine rotationEngine)
        {
            _directionVariable = directionVariable;
            _rotationEngine = rotationEngine;
        }

        public void Enter()
        {
            _directionVariable.Subscribe(Rotate);
        }

        public void Exit()
        {
            _directionVariable.Unsubscribe(Rotate);
        }

        public void Rotate(Vector3 direction)
        {
            _rotationEngine.UpdateRotation(direction, _rotationType);
        }
    }
}