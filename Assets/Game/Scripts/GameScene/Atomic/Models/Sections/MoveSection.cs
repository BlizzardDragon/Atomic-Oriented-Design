using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public sealed class MoveSection
    {
        public AtomicVariable<float> Speed = new();
        public AtomicVariable<bool> MovementAllowed = new();

        public AtomicEvent<Vector3> MoveRequest = new();
        public MoveEngine MoveEngine = new();


        [Construct]
        public void Construct(TransformSection gameObj, LifeSection life)
        {
            MovementAllowed.Value = true;
            MoveEngine.Construct(gameObj.Transform, Speed);

            MoveRequest.Subscribe(direction =>
            {
                if (!MovementAllowed.Value) return;

                if (life.IsAlive.Value)
                {
                    MoveEngine.Move(direction);
                }
            });
        }
    }
}