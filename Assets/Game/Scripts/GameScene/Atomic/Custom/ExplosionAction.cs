using System.Collections.Generic;
using Atomic;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public struct ExplosionArguments
    {
        public Vector3 Position;
        public float Radius;
        public int Damage;
        public LayerMask LayerMask;
    }

    public sealed class ExplosionAction : IAtomicAction<ExplosionArguments>
    {
        public AtomicEvent<Entity[]> AffectedEntitiesEvent;


        public void Invoke(ExplosionArguments args)
        {
            Collider[] colliders = Physics.OverlapSphere(args.Position, args.Radius, args.LayerMask);

            if (colliders.Length < 0) return;

            List<Entity> affectedEntities = new();

            foreach (var collider in colliders)
            {
                if (collider.attachedRigidbody)
                {
                    if (collider.attachedRigidbody.TryGetComponent(out Entity entity))
                    {
                        if (entity.TryGet(out TakeDamageComponent component))
                        {
                            component.TakeDamage(args.Damage);
                            affectedEntities.Add(entity);
                        }
                    }
                }
            }

            if (affectedEntities.Count > 0)
            {
                AffectedEntitiesEvent?.Invoke(affectedEntities.ToArray());
            }
        }
    }
}