using Atomic;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class BulletCollisionMechanics
    {
        private readonly AtomicVariable<int> _damage;
        private readonly AtomicVariable<Teams> _team;
        private readonly GameObject _bullet;

        public BulletCollisionMechanics(
            AtomicVariable<int> damage,
            AtomicVariable<Teams> team,
            GameObject bullet)
        {
            _damage = damage;
            _team = team;
            _bullet = bullet;
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (!collider.attachedRigidbody) return;


            if (!collider.attachedRigidbody.TryGetComponent(out Entity entity))
            {
                return;
            }

            if (entity.TryGet(out ITeamComponent teamComponent) &&
                teamComponent.Team != _team.Value)
            {
                var takeDamageComponent = entity.Get<ITakeDamageComponent>();
                takeDamageComponent?.TakeDamage(_damage);

                GameObject.Destroy(_bullet, 0.01f);
            }
        }
    }
}