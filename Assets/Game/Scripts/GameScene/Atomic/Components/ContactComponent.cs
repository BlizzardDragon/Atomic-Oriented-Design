using Atomic;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public interface IContactComponent
    {
        void OnContact(Entity entity);
    }

    public class ContactComponent : IContactComponent
    {
        private AtomicEvent<Entity> _onContact;

        public ContactComponent(AtomicEvent<Entity> onContact)
        {
            _onContact = onContact;
        }

        public void OnContact(Entity entity)
        {
            _onContact?.Invoke(entity);
        }
    }
}