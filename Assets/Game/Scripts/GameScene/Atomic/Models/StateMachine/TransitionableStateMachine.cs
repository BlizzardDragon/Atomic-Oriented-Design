using System;
using System.Collections.Generic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class TransitionableStateMachine<T> : StateMachine<T>, IUpdate
    {
        private List<(T, Func<bool>)> _transitions = new();

        internal void AddTransition(T key, Func<bool> condition)
        {
            _transitions.Add(new(key, condition));
        }

        public void Update(float deltaTime)
        {
            foreach (var (stateType, condition) in _transitions)
            {
                if (!stateType.Equals(currentStateType) && condition.Invoke())
                {
                    Debug.Log(currentStateType);
                    SwitchState(stateType);
                }
            }
        }
    }
}