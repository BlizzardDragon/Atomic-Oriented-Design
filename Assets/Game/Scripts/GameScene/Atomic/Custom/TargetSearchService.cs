using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class TargetSearchService
    {
        public bool TryFindNearestTarget(out Transform closestTarget, Vector3 currentPos, float radius, int layerMask)
        {
            Collider[] colliders = Physics.OverlapSphere(currentPos, radius, layerMask);

            closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                Transform targetTransform = collider.transform;
                float distanceToTarget = Vector3.Distance(currentPos, targetTransform.position);

                if (distanceToTarget < closestDistance)
                {
                    closestTarget = targetTransform;
                    closestDistance = distanceToTarget;
                }
            }

            if (closestTarget != null)
            {
                return true;
            }

            return false;
        }

        public bool TryFindRandomTarget(out Transform randomTarget, Vector3 currentPos, float radius, int layerMask)
        {
            Collider[] colliders = Physics.OverlapSphere(currentPos, radius, layerMask);

            randomTarget = colliders[Random.Range(0, colliders.Length)].transform;

            if (randomTarget != null)
            {
                return true;
            }

            return false;
        }
    }
}