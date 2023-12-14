using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class EntityTriggerObserver : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private EntityTriggerEnter _trigger;
        private ContactComponent _entityContactComponent;


        private void Start()
        {
            _entityContactComponent = _entity.Get<ContactComponent>();
            _trigger.OnEntityTriggerEnter += OnContact;
        }

        private void OnDisable() => _trigger.OnEntityTriggerEnter -= OnContact;

        private void OnContact(Entity entity) => _entityContactComponent.OnContact(entity);
    }
}