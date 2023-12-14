using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class MoveState : IState
    {
        private MovementDirectionVariable _directionVariable;
        private MoveEngine _moveEngine;


        public void Construct(MovementDirectionVariable directionVariable, MoveEngine moveEngine)
        {
            _directionVariable = directionVariable;
            _moveEngine = moveEngine;
        }

        public void Enter()
        {
            _directionVariable.Subscribe(Move);
        }

        public void Exit()
        {
            _directionVariable.Unsubscribe(Move);
        }

        public void Move(Vector3 direction)
        {
            _moveEngine.Move(direction);
        }
    }
}