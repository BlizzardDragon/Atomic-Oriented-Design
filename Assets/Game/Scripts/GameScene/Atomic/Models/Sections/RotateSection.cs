using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class RotateSection
    {
        [SerializeField] private RotationType _rotationType = RotationType.RotateTowards;

        public AtomicVariable<float> LerpSpeed = new();
        public AtomicVariable<float> TowardsSpeed = new();
        public AtomicEvent<Vector3> RotationRequest = new();

        public RotationEngine RotationEngine = new();


        [Construct]
        public void Construct(TransformSection gameObject)
        {
            RotationEngine.Construct(gameObject.Transform, LerpSpeed, TowardsSpeed);

            RotationRequest.Subscribe(direction =>
            {
                RotationEngine.UpdateRotation(direction, _rotationType);
            });
        }
    }
}