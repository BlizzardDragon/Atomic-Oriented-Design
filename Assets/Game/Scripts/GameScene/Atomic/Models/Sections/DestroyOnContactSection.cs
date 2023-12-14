using System;
using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class DestroyOnContactSection
    {
        public AtomicEvent<Entity> OnContact = new();
        public DestroyService DestroyService;


        [Construct]
        public void Construct(TransformSection transform)
        {
            OnContact.Subscribe(entity =>
            {
                if (entity.TryGet(out ContactComponent contactComponent))
                {
                    DestroyService.DestroyGameObject(transform.GameObject);
                }
            });
        }
    }
}