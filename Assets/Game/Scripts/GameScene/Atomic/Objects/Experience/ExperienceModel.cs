using Atomic;
using Declarative;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class ExperienceModel : DeclarativeModel
    {
        public AtomicVariable<int> ExperienceAmoun = new();

        [Section][SerializeField] public TransformSection TransformSection;


        [Inject]
        public void Construct(Vector3 position, Quaternion quaternion, int amount)
        {
            TransformSection.Transform.position = position;
            TransformSection.Transform.rotation = quaternion;
            ExperienceAmoun.Value = amount;
        }

        public sealed class Factory : PlaceholderFactory<Vector3, Quaternion, int, ExperienceModel> { }
    }
}
