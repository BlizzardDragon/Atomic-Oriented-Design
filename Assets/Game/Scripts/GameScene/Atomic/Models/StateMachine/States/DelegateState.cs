using System;

namespace AtomicOrientedDesign.Shooter
{
    public class DelegateState : IState
    {
        private Action _onEnter;
        private Action _onExit;

        public DelegateState(Action onEnter, Action onExit)
        {
            _onEnter = onEnter;
            _onExit = onExit;
        }


        public void Enter()
        {
            _onEnter?.Invoke();
        }

        public void Exit()
        {
            _onExit?.Invoke();
        }
    }
}