using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PhotonBulletEntity : Entity
    {
        [SerializeField] private PhotonBulletMudel _model;

        private void Awake()
        {
            Add(new DamageComponent(_model.Damage));
            Add(new MoveDirectionComponent(_model.MoveDirection));
        }
    }
}