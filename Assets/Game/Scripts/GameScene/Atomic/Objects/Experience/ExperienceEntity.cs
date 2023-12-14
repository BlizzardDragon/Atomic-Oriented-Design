using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class ExperienceEntity : Entity
    {
        [SerializeField] private ExperienceModel _model;

        private void Awake()
        {
            Add(new ExperienceContainerComponent(_model.ExperienceAmoun));
            Add(new TransformComponent(_model.TransformSection.Transform));
        }
    }
}
