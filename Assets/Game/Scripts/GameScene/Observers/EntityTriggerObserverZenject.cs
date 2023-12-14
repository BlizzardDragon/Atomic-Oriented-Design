using FrameworkUnity.OOP.Interfaces.Listeners;
using Lessons.Gameplay.Atomic2;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class EntityTriggerObserverZenject : IStartGameListener, IDeInitGameListener
    {
        private Entity _entity;
        private EntityTriggerEnter _triggerEnter;
        private ContactComponent _contactComponent;


        [Inject]
        public void Construct(Entity entity, EntityTriggerEnter triggerEnter)
        {
            _entity = entity;
            _triggerEnter = triggerEnter;
        }

        public void OnStartGame()
        {
            _contactComponent = _entity.Get<ContactComponent>();
            _triggerEnter.OnEntityTriggerEnter += OnContact;
        }

        public void OnDeInitGame() => _triggerEnter.OnEntityTriggerEnter -= OnContact;

        private void OnContact(Entity entity) => _contactComponent.OnContact(entity);
    }
}
