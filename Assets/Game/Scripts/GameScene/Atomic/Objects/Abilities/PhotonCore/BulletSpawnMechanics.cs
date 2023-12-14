using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class BulletSpawnMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<Transform> _currentTransform;
        private readonly AtomicVariable<Entity> _target;
        private readonly AtomicVariable<int> _damage;

        private readonly AtomicEvent _fireRequest;

        private readonly GameObject _bulletPrefab;

        public BulletSpawnMechanics(
            AtomicVariable<Transform> currentTransform,
            AtomicVariable<Entity> target,
            AtomicVariable<int> damage,
            AtomicEvent fireRequest,
            GameObject bulletPrefab)
        {
            _currentTransform = currentTransform;
            _target = target;
            _damage = damage;
            _fireRequest = fireRequest;
            _bulletPrefab = bulletPrefab;
        }


        public void OnEnable() => _fireRequest.Subscribe(Shoot);
        public void OnDisable() => _fireRequest.Unsubscribe(Shoot);

        private void Shoot()
        {
            var direction = _target.Value.transform.position - _currentTransform.Value.position;
            direction.y = 0;
            direction.Normalize();

            var newBullet = GameObject.Instantiate(
                _bulletPrefab,
                _currentTransform.Value.position,
                Quaternion.LookRotation(direction),
                null);
            
            var entity = newBullet.GetComponent<Entity>();
            entity.Get<DamageComponent>().SetDamage(_damage);
            entity.Get<MoveDirectionComponent>().SetMoveDirection(direction);
        }
    }
}