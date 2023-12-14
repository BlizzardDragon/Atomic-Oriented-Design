using System;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    public class EntitySearchService
    {
        public bool TryFindNearestEntity(
            out Entity closestEntity,
            Vector3 currentPos,
            float radius,
            int layerMask)
        {
            Collider[] colliders = Physics.OverlapSphere(currentPos, radius, layerMask);

            closestEntity = null;

            Collider closestCollider = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distanceToTarget = Vector3.Distance(currentPos, collider.transform.position);

                if (distanceToTarget < closestDistance)
                {
                    closestCollider = collider;
                    closestDistance = distanceToTarget;
                }
            }

            if (closestCollider != null)
            {
                if (closestCollider.attachedRigidbody)
                {
                    closestEntity = closestCollider.attachedRigidbody.GetComponent<Entity>();
                }
                else
                {
                    throw new InvalidOperationException("Rigidbody is not attached!");
                }

                return true;
            }

            return false;
        }

        public bool TryFindRandomEntity(
            out Entity randomEntity,
            Vector3 currentPos,
            float radius,
            int layerMask)
        {
            randomEntity = null;

            Collider[] colliders = Physics.OverlapSphere(currentPos, radius, layerMask);

            if (colliders.Length != 0)
            {
                var collider = colliders[Random.Range(0, colliders.Length)];

                if (collider.attachedRigidbody)
                {
                    randomEntity = collider.attachedRigidbody.GetComponent<Entity>();
                }
                else
                {
                    throw new InvalidOperationException("Rigidbody is not attached!");
                }
            }

            if (randomEntity != null)
            {
                return true;
            }

            return false;
        }
    }
}