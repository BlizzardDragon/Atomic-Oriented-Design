using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    [Serializable]
    public class FireGrenadeModel_Core
    {
        [Section][SerializeField] public TransformSection TransformSection;
        [Section][SerializeField] public MovementFromToSection MoveFromTo;
        [Section][SerializeField] public ExplosionSection Explosion;


        [Serializable]
        public class MovementFromToSection
        {
            public AtomicVariable<float> Speed = new();
            public AtomicVariable<Vector3> TargetPosition = new();

            public AtomicEvent OnMovementFinished;

            public UpdateMechanics Update = new();


            [Construct]
            public void Construct(TransformSection transformSection)
            {
                var currentTransform = transformSection.Transform;

                Update.OnUpdate(deltaTime =>
                {
                    if (currentTransform.position == TargetPosition)
                    {
                        OnMovementFinished?.Invoke();
                        return;
                    }

                    currentTransform.position =
                        Vector3.MoveTowards(currentTransform.position, TargetPosition, Speed * deltaTime);
                });

                OnMovementFinished.Subscribe(() =>
                {
                    Update.OnUpdate(_ => { });
                });
            }
        }

        [Serializable]
        public class ExplosionSection
        {
            public AtomicVariable<int> Damage = new();
            public AtomicVariable<float> ExplosionRadius = new();

            public AtomicEvent OnExplosion = new();

            public LayerMask LayerMask;

            private ExplosionAction _explosionAction = new();

            private DestroyService _destroyService = new DestroyService();


            [Construct]
            public void Construct(TransformSection transformSection, MovementFromToSection movementFromTo)
            {
                movementFromTo.OnMovementFinished.Subscribe(() =>
                {
                    _explosionAction.Invoke(new ExplosionArguments{
                        Position = transformSection.Transform.transform.position,
                        Radius = ExplosionRadius,
                        Damage = Damage,
                        LayerMask = LayerMask
                    });

                    OnExplosion?.Invoke();
                });

                OnExplosion.Subscribe(() => _destroyService.DestroyGameObject(transformSection.GameObject));
            }
        }
    }
}