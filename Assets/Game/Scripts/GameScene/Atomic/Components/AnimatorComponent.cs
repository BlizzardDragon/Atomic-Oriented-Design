using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    interface IAnimatorComponent
    {
        Animator Animator{ get; }
    }

    public class AnimatorComponent : IAnimatorComponent
    {
        public Animator Animator => _animator;
        private Animator _animator;

        public AnimatorComponent(Animator animator)
        {
            _animator = animator;
        }
    }
}