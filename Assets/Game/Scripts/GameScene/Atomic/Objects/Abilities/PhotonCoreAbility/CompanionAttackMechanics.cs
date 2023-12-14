using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class CompanionAttackMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<Entity> _companion;
        private readonly AtomicVariable<Entity> _attackTarget;
        private readonly AtomicEvent _attackRequest;

        public CompanionAttackMechanics(
            AtomicVariable<Entity> companion,
            AtomicVariable<Entity> attackTarget,
            AtomicEvent attackRequest)
        {
            _companion = companion;
            _attackTarget = attackTarget;
            _attackRequest = attackRequest;
        }


        public void OnEnable() => _attackRequest.Subscribe(OnAttack);
        public void OnDisable() => _attackRequest.Unsubscribe(OnAttack);

        private void OnAttack()
        {
            _companion.Value.Get<TargetComponent>().SetTarget(_attackTarget);
            _companion.Value.Get<FireRequestComponent>().FireRequest();
        }
    }
}