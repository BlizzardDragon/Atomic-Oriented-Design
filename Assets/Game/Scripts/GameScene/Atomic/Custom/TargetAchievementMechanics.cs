using Atomic;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class TargetAchievementMechanics
    {
        public void Construct(
            AtomicEvent moveToTargetRequest,
            AtomicVariable<Transform> targetTransform,
            Transform currentTransform,
            AtomicVariable<float> distanceAttack,
            AtomicEvent<Entity> onTargetAchieved,
            AtomicVariable<Entity> targetEntity,
            AtomicEvent<Vector3> moveRequest)
        {
            moveToTargetRequest.Subscribe(() =>
            {
                Vector3 currentPos = currentTransform.position;
                Vector3 target = targetTransform.Value.position;

                if (Vector3.Distance(currentPos, target) <= distanceAttack.Value)
                {
                    onTargetAchieved?.Invoke(targetEntity.Value);
                }
                else
                {
                    Vector3 direction = target - currentPos;
                    direction.y = 0;
                    moveRequest?.Invoke(direction);
                }
            });
        }
    }
}