using System;
using Game.GameEngine.Animation;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class EnemyAnimatioMessageObserver : IDisposable
    {
        private AnimatorObservable _observer;
        private EnemyEntity _entity;
        private SendMessageComponent _messageReceivingComponent;


        [Inject]
        public void Construct(AnimatorObservable animatorObservable, EnemyEntity enemyEntity)
        {
            _observer = animatorObservable;
            _entity = enemyEntity;
        }

        internal void Init()
        {
            _messageReceivingComponent = _entity.Get<SendMessageComponent>();
            _observer.OnStringReceived += SendMessage;
        }

        public void Dispose() => _observer.OnStringReceived -= SendMessage;

        private void SendMessage(string message)
        {
            _messageReceivingComponent.SendMessage(message);
        }
    }
}
