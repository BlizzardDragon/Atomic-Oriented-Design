using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class FreezingMechanics : IEnable, IDisable
    {
        private readonly AtomicVariable<float> _freezeTime;
        private readonly AtomicVariable<float> _delayBeforeFreezing;
        private readonly AtomicVariable<float> _minSpeed;

        private readonly AtomicEvent<Entity[]> _affectedEntitiesEvent;

        private readonly DestroyService _destroyService = new();

        public FreezingMechanics(
            AtomicVariable<float> freezeTime,
            AtomicVariable<float> delayBeforeFreezing,
            AtomicVariable<float> minSpeed,
            AtomicEvent<Entity[]> affectedEntitiesEvent)
        {
            _freezeTime = freezeTime;
            _delayBeforeFreezing = delayBeforeFreezing;
            _minSpeed = minSpeed;
            _affectedEntitiesEvent = affectedEntitiesEvent;
        }


        public void OnEnable() => _affectedEntitiesEvent.Subscribe(FreezeEntities);
        public void OnDisable() => _affectedEntitiesEvent.Unsubscribe(FreezeEntities);

        private void FreezeEntities(Entity[] entities)
        {
            foreach (var entity in entities)
            {
                var oldFreezeEffect = entity.GetComponent<FreezeEffect>();
                if (oldFreezeEffect)
                {
                    _destroyService.DestroyObject(oldFreezeEffect);
                }

                var freezeEffect = entity.gameObject.AddComponent<FreezeEffect>();
                freezeEffect.StartFreezing(_delayBeforeFreezing, _freezeTime, _minSpeed);
            }
        }
    }
}