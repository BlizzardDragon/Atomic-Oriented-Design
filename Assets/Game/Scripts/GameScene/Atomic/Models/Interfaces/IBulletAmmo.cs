using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface IBulletAmmo
    {
        public AtomicVariable<int> NumberBullets { get; set; }
        public int ClipSize { get; set; }
        public bool IsNotEmpty => NumberBullets.Value > 0;
        public bool IsFull => NumberBullets.Value >= ClipSize;
    }
}