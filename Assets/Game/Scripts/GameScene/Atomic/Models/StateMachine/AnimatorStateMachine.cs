using System;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class AnimatorStateMachine<T> : TransitionableStateMachine<T> where T : Enum
    {
        private static readonly int State = Animator.StringToHash("State");

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _dispatcher;
        
        public event Action<string> OnMessageReceived
        {
            add { _dispatcher.OnMessageReceived += value; }
            remove { _dispatcher.OnMessageReceived -= value; }
        }

        public override void SwitchState(T stateType)
        {
            base.SwitchState(stateType);
            _animator.SetInteger(State, Convert.ToInt32(stateType));
        }
    }
}