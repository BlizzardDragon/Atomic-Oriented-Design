using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class TakeDamageState : IState
    {
        private const string TRIGGER_NAME = "TakeDamage";
        private AtomicEvent<int> _takeDamageEvent = new();
        private Animator _animator;
        private int _triggerId;


        [Construct]
        public void Construct(Animator animator, AtomicEvent<int> takeDamageEvent)
        {
            _triggerId = Animator.StringToHash(TRIGGER_NAME);
            _animator = animator;
            _takeDamageEvent = takeDamageEvent;
        }

        public void Enter()
        {
            _takeDamageEvent.Subscribe(_ => OnTakeDamage());
        }

        public void Exit()
        {
            _takeDamageEvent.Unsubscribe(_ => OnTakeDamage());
        }

        private void OnTakeDamage()
        {
            _animator.SetTrigger(_triggerId);
        }
    }
}