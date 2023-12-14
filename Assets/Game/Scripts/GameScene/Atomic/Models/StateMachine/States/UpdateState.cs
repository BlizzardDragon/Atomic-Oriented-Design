using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public abstract class UpdateState : IState, IUpdate
    {
        private bool _isEnable;


        public void Enter()
        {
            _isEnable = true;
            OnEnter();
        }

        public void Exit()
        {
            _isEnable = false;
            OnExit();
        }

        public void Update(float deltaTime)
        {
            if (_isEnable)
            {
                OnUpdate(deltaTime);
            }
        }

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected abstract void OnUpdate(float deltaTime);
    }
}