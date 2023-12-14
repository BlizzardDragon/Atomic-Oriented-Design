using System;
using Declarative;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class ScatterState : UpdateState
    {
        [SerializeField] private RotationType _rotationType = RotationType.Slerp;

        private Vector3 _randomDirection;
        private RotationEngine _rotation;
        private MoveEngine _move;


        public void Construct(MoveEngine move, RotationEngine rotation)
        {
            _move = move;
            _rotation = rotation;

            _randomDirection = Random.onUnitSphere;
            _randomDirection.y = 0;
            _randomDirection.Normalize();
        }

        protected override void OnUpdate(float deltaTime)
        {
            _move.Move(_randomDirection);
            _rotation.UpdateRotation(_randomDirection, _rotationType);
        }
    }
}