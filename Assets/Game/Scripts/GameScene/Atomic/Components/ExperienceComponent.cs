using System;
using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IExperienceComponent
    {
        int CurrentExperience { get; }
        int RequiredExperience { get; }

        event Action<int> OnCurrentExperienceChanged;
        event Action<int> OnRequiredExperienceChanged;
    }

    public sealed class ExperienceComponent : IExperienceComponent
    {
        public int CurrentExperience => _currentExperience.Value;
        public int RequiredExperience => _requiredExperience.Value;

        private readonly AtomicVariable<int> _currentExperience;
        private readonly AtomicVariable<int> _requiredExperience;

        public event Action<int> OnCurrentExperienceChanged
        {
            add => _currentExperience.Subscribe(value);
            remove => _currentExperience.Unsubscribe(value);
        }
        public event Action<int> OnRequiredExperienceChanged
        {
            add => _requiredExperience.Subscribe(value);
            remove => _requiredExperience.Unsubscribe(value);
        }

        public ExperienceComponent(
            AtomicVariable<int> currentExperience,
            AtomicVariable<int> requiredExperience)
        {
            _currentExperience = currentExperience;
            _requiredExperience = requiredExperience;
        }
    }
}