using System;
using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class TimeScaleController : IStartGameListener, IDisposable
    {
        private TimeScaleManager _timeScaleManager;
        private PlayerEntity _playerEntity;
        private CardView[] _cardViews;


        [Inject]
        public void Construct(
            TimeScaleManager timeScaleManager,
            PlayerEntity playerEntity,
            CardView[] cardViews)
        {
            _timeScaleManager = timeScaleManager;
            _playerEntity = playerEntity;
            _cardViews = cardViews;
        }

        public void OnStartGame()
        {
            _playerEntity.Get<LevelComponent>().OnLevelChanged += Pause;

            foreach (var cardView in _cardViews)
            {
                cardView.OnClicked += TryContinue;
            }
        }

        public void Dispose()
        {
            _playerEntity.Get<LevelComponent>().OnLevelChanged -= Pause;

            foreach (var cardView in _cardViews)
            {
                cardView.OnClicked -= TryContinue;
            }

            _timeScaleManager.PlayTime();
        }

        private void TryContinue(CardView _)
        {
            _timeScaleManager.TryPlayTime(nameof(TimeScaleController));
        }

        private void Pause(int _)
        {
            _timeScaleManager.StopTime(nameof(TimeScaleController));
        }
    }
}