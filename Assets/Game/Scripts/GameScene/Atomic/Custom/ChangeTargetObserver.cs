using Atomic;
using Lessons.Gameplay.Atomic2;
using Unity.VisualScripting;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public class ChangeTargetObserver
    {
        public void Construct(
            AtomicEvent<Entity> targetFound,
            AtomicVariable<Entity> targetEntity,
            AtomicVariable<Transform> targetTransform,
            AtomicEvent targetIsDead,
            AtomicEvent onDeath)
        {
            targetFound.Subscribe(Entity =>
            {
                var hitPointsComponent = Entity.Get<HitPointsComponent>();
                var lifeComponent = Entity.Get<LifeComponent>();
                
                if (hitPointsComponent.HitPoints <= 0)
                {
                    targetIsDead?.Invoke();
                }
                else
                {
                    targetTransform.Value = Entity.Get<TransformComponent>().EntityTransform;
                    lifeComponent.OnDeath += () => targetIsDead?.Invoke();
                }
            });

            onDeath.Serialize(targetEntity.Value = null);
            targetIsDead.Serialize(targetEntity.Value = null);
        }
    }
}