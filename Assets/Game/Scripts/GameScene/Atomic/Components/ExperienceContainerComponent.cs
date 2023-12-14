using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IExperienceContainerComponent
    {
        public int ExperienceAmount { get;}
    }

    public class ExperienceContainerComponent : IExperienceContainerComponent
    {
        public int ExperienceAmount => _experienceAmount.Value;
        
        private AtomicVariable<int> _experienceAmount;

        public ExperienceContainerComponent(AtomicVariable<int> experienceAmount)
        {
            _experienceAmount = experienceAmount;
        }
    }
}