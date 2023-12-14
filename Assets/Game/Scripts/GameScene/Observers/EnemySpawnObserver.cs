using System;
using System.Collections.Generic;
using FrameworkUnity.OOP.Interfaces.Listeners;
using Zenject;

namespace AtomicOrientedDesign.Shooter
{
    public class EnemySpawnObserver : IStartGameListener, ILoseGameListener
    {
        private EnemySpawner _enemySpawner;
        private PlayerEntity _playerEntity;
        private ScoreManager _scoreManager;
        private Dictionary<LifeComponent, Action> _enemyDeathHandlers = new();


        [Inject]
        public void Construct(EnemySpawner enemySpawner, PlayerEntity playerEntity, ScoreManager scoreManager)
        {
            _enemySpawner = enemySpawner;
            _playerEntity = playerEntity;
            _scoreManager = scoreManager;
        }

        public void OnStartGame() => _enemySpawner.OnEnemySpawned += InstallEnemy;
        public void OnLoseGame() => _enemySpawner.OnEnemySpawned -= InstallEnemy;

        private void InstallEnemy(EnemyEntity enemy)
        {
            enemy.Get<TargetComponent>().SetTarget(_playerEntity);

            LifeComponent currentLifeComponent = enemy.Get<LifeComponent>();
            Action deathHandler = () =>
            {
                _scoreManager.AddKillsScore();
                currentLifeComponent.OnDeath -= _enemyDeathHandlers[currentLifeComponent];
                _enemyDeathHandlers.Remove(currentLifeComponent);
            };

            currentLifeComponent.OnDeath += deathHandler;
            _enemyDeathHandlers[currentLifeComponent] = deathHandler;
        }
    }
}