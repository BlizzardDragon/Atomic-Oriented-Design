using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class EnemyRunState : UpdateState
    {
        private AtomicEvent _moveToTarget = new();

        public void Construct(AtomicEvent moveToTarget) => _moveToTarget = moveToTarget;
        protected override void OnUpdate(float _) => _moveToTarget?.Invoke();
    }
}