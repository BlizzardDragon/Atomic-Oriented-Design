using FrameworkUnity.OOP.Interfaces.Listeners;
using FrameworkUnity.OOP.Zenject;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class GameOverController : IStartGameListener, ILoseGameListener
    {
        private PlayerEntity _entity;
        private GameManagerZenject _gameManager;


        [Inject]
        public void Construct(PlayerEntity playerEntity, GameManagerZenject gameManager)
        {
            _entity = playerEntity;
            _gameManager = gameManager;
        }

        public void OnLoseGame() => _entity.Get<LifeComponent>().OnDeath += _gameManager.LoseGame;
        public void OnStartGame() => _entity.Get<LifeComponent>().OnDeath -= _gameManager.LoseGame;
    }
}