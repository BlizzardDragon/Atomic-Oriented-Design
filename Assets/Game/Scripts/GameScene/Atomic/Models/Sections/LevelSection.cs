using System;
using Atomic;
using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class LevelSection
    {
        public AtomicVariable<int> Level = new();
        public AtomicVariable<int> CurrentExperience = new();
        public AtomicVariable<int> RequiredExperience = new();

        public AtomicEvent<int> AddExperience = new();

        public IExperienceConfig ExperienceConfig;


        [Construct]
        public void Construct()
        {
            Level.Value = 1;
            RequiredExperience.Value = ExperienceConfig.GetRequiredExperience(Level.Value);

            AddExperience.Subscribe(value =>
            {
                if (value == 0) return;

                if (CurrentExperience.Value + value >= RequiredExperience.Value)
                {
                    CurrentExperience.Value = 0;
                    Level.Value++;
                    RequiredExperience.Value = ExperienceConfig.GetRequiredExperience(Level.Value);
                }
                else
                {
                    CurrentExperience.Value += value;
                }
            });
        }
    }
}