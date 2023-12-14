using Atomic;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class PhotonBulletMudel : MonoBehaviour
    {
        // Data:
        public AtomicVariable<int> Damage;
        public AtomicVariable<float> Speed;
        public AtomicVariable<float> Lifetime = new(3);
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<bool> CanMove = new(true);
        public AtomicVariable<Teams> Team;

        // Logic:
        private MovementMechanics _movementMechanics;
        private BulletCollisionMechanics _collisionMechanics;
        private BulletLifetimeMechanics _lifetimeMechanics;

        private void Awake()
        {
            _movementMechanics = new MovementMechanics(CanMove, Speed, MoveDirection, transform);
            _collisionMechanics = new BulletCollisionMechanics(Damage, Team, gameObject);
            _lifetimeMechanics = new BulletLifetimeMechanics(Lifetime, gameObject);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _movementMechanics.Update(deltaTime);
            _lifetimeMechanics.Update(deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _collisionMechanics.OnTriggerEnter(other);
        }
    }
}