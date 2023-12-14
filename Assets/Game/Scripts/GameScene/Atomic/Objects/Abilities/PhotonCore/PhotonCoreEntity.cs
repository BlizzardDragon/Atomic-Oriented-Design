using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PhotonCoreEntity : Entity
    {
        [SerializeField] private PhotonCoreModel _model;

        private void Awake()
        {
            Add(new DamageComponent(_model.Damage));
            Add(new FireRequestComponent(_model.FireRequest));
            Add(new TargetComponent(_model.Target));
        }
    }
}