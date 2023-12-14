using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class BulletEntity : Entity
    {
        [SerializeField] private BulletModel _model;

        internal void Awake()
        {
            Add(new ContactComponent(_model.Contact.OnContact));
        }
    }
}