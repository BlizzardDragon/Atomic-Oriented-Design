using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IDamageComponent
    {
        int Damage { get; }
        void SetDamage(int damage);
    }

    public class DamageComponent : IDamageComponent
    {
        public int Damage => _damage.Value;
        private AtomicVariable<int> _damage;

        public DamageComponent(AtomicVariable<int> damage)
        {
            _damage = damage;
        }

        public void SetDamage(int damage) => _damage.Value = damage;
    }
}