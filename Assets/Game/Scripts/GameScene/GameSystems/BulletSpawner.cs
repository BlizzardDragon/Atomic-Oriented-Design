using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class BulletSpawner
    {
        private BulletModel.Factory _factory;


        [Inject]
        public void Construct(BulletModel.Factory factory) => _factory = factory;

        public void SpawnBullet(BulletArguments bulletArg)
        {
            Vector3 eulerAngles = new Vector3(0, bulletArg.Rotation.eulerAngles.y, 0);
            Quaternion rotation = Quaternion.Euler(eulerAngles);
            _factory.Create(bulletArg.Position, rotation, bulletArg.Team, bulletArg.Damage);
        }
    }
}