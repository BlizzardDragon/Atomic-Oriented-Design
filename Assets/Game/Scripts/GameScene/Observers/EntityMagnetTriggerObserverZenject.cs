using FrameworkUnity.OOP.Interfaces.Listeners;
using Lessons.Gameplay.Atomic2;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class EntityMagnetTriggerObserverZenject : IStartGameListener, IDeInitGameListener
    {
        private Entity _entity;
        private RigidbodyTriggerEnter _triggerEnter;

        private MagnetComponent _magnetComponent;


        [Inject]
        public void Construct(Entity entity, RigidbodyTriggerEnter triggerEnter)
        { 
            _entity = entity;
            _triggerEnter = triggerEnter;
        }

        public void OnStartGame()
        {
            _magnetComponent = _entity.Get<MagnetComponent>();
            _triggerEnter.OnColliderEnter += TryAddToMagnet;
        }

        public void OnDeInitGame() => _triggerEnter.OnColliderEnter -= TryAddToMagnet;

        private void TryAddToMagnet(Transform target) => _magnetComponent.TryAddTarget(target);
    }
}