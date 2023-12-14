using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public interface IAbilityUserComponent
    {
        Transform ParentTransform { get; }
    }

    public class AbilityUserComponent : IAbilityUserComponent
    {
        public Transform ParentTransform => _parentTransform;
        private Transform _parentTransform;

        public AbilityUserComponent(Transform parentTransform)
        {
            _parentTransform = parentTransform;
        }
    }
}