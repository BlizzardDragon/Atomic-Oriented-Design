using System;

namespace AtomicOrientedDesign.Shooter
{
    public interface IBulletAmmoComponent
    {
        int NumberBullets { get; }
        int ClipSize { get; }
        bool IsNotEmpty { get; }
        bool IsFull { get; }
        event Action<int> OnNumberBulletsChanged;
    }

    public class BulletAmmoComponent : IBulletAmmoComponent
    {

        public int NumberBullets => _bulletAmmo.NumberBullets;
        public int ClipSize => _bulletAmmo.ClipSize;
        public bool IsNotEmpty => _bulletAmmo.IsNotEmpty;
        public bool IsFull => _bulletAmmo.IsFull;

        public event Action<int> OnNumberBulletsChanged 
        {
            add => _bulletAmmo.NumberBullets.Subscribe(value);
            remove => _bulletAmmo.NumberBullets.Unsubscribe(value);
        }

        private IBulletAmmo _bulletAmmo;

        public BulletAmmoComponent(IBulletAmmo bulletAmmo)
        {
            _bulletAmmo = bulletAmmo;
        }
    }
}