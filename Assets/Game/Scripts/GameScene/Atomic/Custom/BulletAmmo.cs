using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public class BulletAmmo : IBulletAmmo
    {
        public AtomicVariable<int> NumberBullets { get; set; } = new();
        public int ClipSize { get; set; }
    }
}