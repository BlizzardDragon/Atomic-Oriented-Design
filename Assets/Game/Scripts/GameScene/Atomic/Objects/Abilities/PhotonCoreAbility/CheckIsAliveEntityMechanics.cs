using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class CheckIsAliveEntityMechanics : IEnable, IDisable
    {
        private readonly AtomicEvent _checkIsAliveRequest;
        private readonly AtomicEvent _searchRequest;
        private readonly AtomicEvent _entityIsAlive;
        private readonly AtomicVariable<Entity> _target;

        public CheckIsAliveEntityMechanics(
            AtomicEvent checkIsAliveRequest,
            AtomicEvent searchRequest,
            AtomicEvent entityIsAlive,
            AtomicVariable<Entity> target)
        {
            _checkIsAliveRequest = checkIsAliveRequest;
            _searchRequest = searchRequest;
            _entityIsAlive = entityIsAlive;
            _target = target;
        }


        public void OnEnable() => _checkIsAliveRequest.Subscribe(CheckIsAlive);
        public void OnDisable() => _checkIsAliveRequest.Unsubscribe(CheckIsAlive);

        private void CheckIsAlive()
        {
            if (_target.Value == null || !_target.Value.Get<LifeComponent>().IsAlive)
            {
                _searchRequest?.Invoke();
            }
            else
            {
                _entityIsAlive?.Invoke();
                return;
            }

            if (_target.Value == null || !_target.Value.Get<LifeComponent>().IsAlive) return;

            _entityIsAlive?.Invoke();
        }
    }
}