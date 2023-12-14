using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class FireBulletAction : AtomicAction<BulletArguments>
    {
        private BulletSpawner _bulletSpawner;

        public FireBulletAction(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }

        public override void Invoke(BulletArguments args)
        {
            base.Invoke(args);
            _bulletSpawner.SpawnBullet(args);
        }
    }
}