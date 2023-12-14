using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class ExperienceSpawner
    {
        private ExperienceModel.Factory _factory;


        [Inject]
        public void Construct(ExperienceModel.Factory factory) => _factory = factory;

        public void SpawnExperience(ExperienceArguments ExperienceArg)
        {
            Vector3 eulerAngles = new Vector3(0, ExperienceArg.Rotation.eulerAngles.y, 0);
            Quaternion rotation = Quaternion.Euler(eulerAngles);
            _factory.Create(ExperienceArg.Position, rotation, ExperienceArg.Amount);
        }
    }
}