using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class PlayerLevelViewObserver : IInitGameListener, IDeInitGameListener
    {
        private PlayerEntity _entity;
        private PlayerLevelView _levelView;
        private LevelComponent _levelComponent;


        [Inject]
        public void Construct(PlayerEntity entity, PlayerLevelView levelView)
        {
            _entity = entity;
            _levelView = levelView;
        }

        public void OnInitGame()
        {
            _levelComponent = _entity.Get<LevelComponent>();
            _levelComponent.OnLevelChanged += UpdateLevel;
            UpdateLevel(_levelComponent.Level);
        }

        public void OnDeInitGame() => _levelComponent.OnLevelChanged -= UpdateLevel;

        private void UpdateLevel(int level) => _levelView.SetLevel(level.ToString());
    }
}
