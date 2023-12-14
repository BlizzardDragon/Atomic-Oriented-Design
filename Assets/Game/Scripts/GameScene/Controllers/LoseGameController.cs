using FrameworkUnity.OOP.Interfaces.Listeners;
using FrameworkUnity.OOP.Zenject;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class LoseGameController : IStartGameListener, ILoseGameListener
    {
        private GameManagerZenject _gameManager;
        private PlayerEntity _playerEntity;


        [Inject]
        public void Construct(GameManagerZenject gameManager, PlayerEntity playerEntity)
        {
            _gameManager = gameManager;
            _playerEntity = playerEntity;
        }

        public void OnStartGame() => _playerEntity.Get<LifeComponent>().OnDeath += LoseGame;
        public void OnLoseGame() => _playerEntity.Get<LifeComponent>().OnDeath -= LoseGame;

        private void LoseGame() => _gameManager.LoseGame();
    }
}