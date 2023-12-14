using Atomic;
using Declarative;
using Lessons.Gameplay.Atomic2;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    public sealed class ExplosionMechanics : IEnable, IDisable
    {
        private readonly AtomicEvent _explosionRequest;

        private readonly AtomicVariable<float> _explosionRadius;
        private readonly AtomicVariable<int> _damage;
        private readonly AtomicVariable<Transform> _currentTransform;

        private readonly LayerMask _layerMask;

        private readonly ExplosionAction explosionAction = new();

        public ExplosionMechanics(
            AtomicEvent explosionRequest,
            AtomicEvent<Entity[]> affectedEntitiesEvent,
            AtomicVariable<float> explosionRadius,
            AtomicVariable<int> damage,
            AtomicVariable<Transform> currentTransform,
            LayerMask layerMask)
        {
            _explosionRequest = explosionRequest;
            explosionAction.AffectedEntitiesEvent = affectedEntitiesEvent;
            _explosionRadius = explosionRadius;
            _damage = damage;
            _currentTransform = currentTransform;
            _layerMask = layerMask;

        }

        public void OnEnable() => _explosionRequest.Subscribe(Explosion);
        public void OnDisable() => _explosionRequest.Unsubscribe(Explosion);

        private void Explosion()
        {
            explosionAction?.Invoke(new ExplosionArguments
            {
                Position = _currentTransform.Value.position,
                Radius = _explosionRadius,
                Damage = _damage,
                LayerMask = _layerMask,
            });
        }
    }
}