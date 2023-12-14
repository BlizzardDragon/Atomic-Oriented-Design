using Atomic;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public interface ITargetComponent
    {
        void SetTarget(Entity target);
    }

    public class TargetComponent : ITargetComponent
    {
        private AtomicVariable<Entity> _target;

        public TargetComponent(AtomicVariable<Entity> target)
        {
            _target = target;
        }
        
        public void SetTarget(Entity target)
        {
            _target.Value = target;
        }
    }
}