using System;
using Atomic;
using Declarative;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class AmmoModel : DeclarativeModel
    {
        [SerializeField] private float _lifeTime = 30;
        public AtomicVariable<int> NubmerAmmo = new();

        [Section][SerializeField] public TransformSection Transform;
        [Section][SerializeField] public new DestroyOnContactSection Destroy;

        [Section][SerializeField] public RotateSection Rotate;


        [Inject]
        public void Construct(DestroyService destroyService) => Destroy.DestroyService = destroyService;

        private void Start() => Destroy(gameObject, _lifeTime);

        [Serializable]
        public class RotateSection
        {
            public AtomicVariable<Vector3> Velocity = new();
            private UpdateMechanics _updateMechanics = new();


            [Construct]
            public void Construct(TransformSection transform)
            {
                _updateMechanics.OnUpdate(deltaTime => transform.Transform.Rotate(Velocity.Value * deltaTime));
            }
        }
    }
}