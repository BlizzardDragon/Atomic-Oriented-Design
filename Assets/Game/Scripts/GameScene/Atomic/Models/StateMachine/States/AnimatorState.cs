using System;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class AnimatorState : IState
    {
        [SerializeField] private AnimatorStateType _stateType;

        [SerializeField] private string stateName = "State";

        private Animator _animator;
        private int _state;


        public void Construct(Animator animator)
        {
            _animator = animator;
            _state = Animator.StringToHash(stateName);
        }

        public void Enter()
        {
            _animator.SetInteger(_state, (int)_stateType);
        }

        public void Exit()
        {
            
        }
    }
}