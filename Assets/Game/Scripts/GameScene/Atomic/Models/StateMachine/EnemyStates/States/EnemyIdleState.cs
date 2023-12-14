using System;
using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using static AtomicOrientedDesign.Shooter.EnemyModel_Core;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class EnemyIdleState : UpdateState
    {
        private AtomicVariable<Entity> _targetEnetity = new();
        private AtomicEvent<Entity> _targetFound = new();


        [Construct]
        public void Construct(TargetSection target)
        {
            _targetEnetity = target.TargetEnetity;
            _targetFound = target.TargetFound;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_targetEnetity.Value != null)
            {
                _targetFound?.Invoke(_targetEnetity.Value);
            }
        }
    }
}