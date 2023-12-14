using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PhotonCoreModel : DeclarativeModel
    {
        // Data:
        [Header("Transforms")]
        public AtomicVariable<Transform> CurrentTransform;
        public AtomicVariable<Transform> RotationCenter;

        [Header("Attack")]
        public GameObject BulletPrefab;
        public AtomicVariable<Entity> Target;
        public AtomicVariable<int> Damage;
        public AtomicEvent FireRequest;

        [Header("Rotation")]
        public AtomicVariable<float> RotationSpeed;
        public AtomicVariable<float> RotationRadius;

        // Logic: 
        private RotationAroundMechanics _rotationAround;
        private BulletSpawnMechanics _bulletMechanics;


        [Construct]
        private void Construct()
        {
            _rotationAround = new RotationAroundMechanics(
                CurrentTransform, RotationCenter, RotationSpeed, RotationRadius);

            _bulletMechanics = new BulletSpawnMechanics(
                CurrentTransform, Target, Damage, FireRequest, BulletPrefab);
        }
    }
}