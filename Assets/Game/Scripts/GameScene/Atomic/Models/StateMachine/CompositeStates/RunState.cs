using System;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class RunState : CompositeState
    {
        public MoveState MoveState = new();
        public RotateState RotateState = new();


        [Construct]
        public void ConstructSelf()
        {
            SetStates(MoveState, RotateState);
        }

        public void ConstructSubStates(
            MovementDirectionVariable directionVariable,
            MoveEngine moveEngine,
            RotationEngine rotationEngine)
        {
            MoveState.Construct(directionVariable, moveEngine);
            RotateState.Construct(directionVariable, rotationEngine);
        }
    }
}