using Declarative;

namespace AtomicOrientedDesign.Shooter
{
    public class Timer : IUpdate
    {
        public float Time { get; private set; }


        public void Update(float deltaTime) => Time += deltaTime;
        public void Reset() => Time = 0;
    }
}