using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IFireRequestComponent
    {
        void FireRequest();
    }

    public class FireRequestComponent : IFireRequestComponent
    {
        private AtomicEvent _onFireRequest;

        public FireRequestComponent(AtomicEvent onFireRequest)
        {
            _onFireRequest = onFireRequest;
        }

        public void FireRequest()
        {
            _onFireRequest?.Invoke();
        }
    }
}