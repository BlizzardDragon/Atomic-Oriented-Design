using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class BulletTriggerObserver : MonoBehaviour
    {
        [SerializeField] private BulletEntity _entity;
        [SerializeField] private EntityTriggerEnter _trigger;
        [SerializeField] private BulletTrail _bulletTrail;
        private ContactComponent _contactComponent;


        private void OnEnable()
        {
            _contactComponent = _entity.Get<ContactComponent>();
            _trigger.OnEntityTriggerEnter += OnContact;
        }

        private void OnDisable() => _trigger.OnEntityTriggerEnter -= OnContact;

        private void OnContact(Entity entity)
        {
            _contactComponent.OnContact(entity);
            _bulletTrail.InvokeDestroyTrail();
        }
    }
}