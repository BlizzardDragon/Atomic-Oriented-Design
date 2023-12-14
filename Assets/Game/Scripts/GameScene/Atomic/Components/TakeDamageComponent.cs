using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface ITakeDamageComponent
    {
        void TakeDamage(int damage);
    }

    public sealed class TakeDamageComponent : ITakeDamageComponent
    {
        private readonly IAtomicAction<int> _onTakeDamage;

        public TakeDamageComponent(IAtomicAction<int> onTakeDamage)
        {
            _onTakeDamage = onTakeDamage;
        }

        public void TakeDamage(int damage)
        {
            _onTakeDamage.Invoke(damage);
        }
    }
}