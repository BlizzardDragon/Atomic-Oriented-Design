using System;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class State : IState
    {
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}