using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IShotScatterMultiplierComponent
    {
        void SetMultiplier(float value);
    }

    public class ShotScatterMultiplierComponent : IShotScatterMultiplierComponent
    {
        private AtomicVariable<float> _shotScatterMultiplier;

        public ShotScatterMultiplierComponent(AtomicVariable<float> shotScatterMultiplier)
        {
            _shotScatterMultiplier = shotScatterMultiplier;
        }

        public void SetMultiplier(float value) => _shotScatterMultiplier.Value = value;
    }
}