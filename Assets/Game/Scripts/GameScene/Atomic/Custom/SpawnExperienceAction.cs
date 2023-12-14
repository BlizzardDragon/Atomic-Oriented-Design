using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class SpawnExperienceAction : AtomicAction<ExperienceArguments>
    {
        private ExperienceSpawner _experienceSpawner;

        public SpawnExperienceAction(ExperienceSpawner experienceSpawner)
        {
            _experienceSpawner = experienceSpawner;
        }

        public override void Invoke(ExperienceArguments args)
        {
            base.Invoke(args);
            _experienceSpawner.SpawnExperience(args);
        }
    }
}