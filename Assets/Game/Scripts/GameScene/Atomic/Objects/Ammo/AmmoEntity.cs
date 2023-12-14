using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class AmmoEntity : Entity
    {
        [SerializeField] private AmmoModel _model;

        private void Awake()
        {
            Add(new AmmoContainerComponent(_model.NubmerAmmo));
            Add(new ContactComponent(_model.Destroy.OnContact));
        }
    }
}