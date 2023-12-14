using System;
using System.Collections;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    public class EnemySpawner : MonoBehaviour, IStartGameListener, ILoseGameListener
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _worldParent;

        private Transform _player;
        private PlayerEntity _playerEntity;
        private Camera _camera;
        private DiContainer _container;
        [SerializeField] private Color _gizmosColor = Color.blue;

        private float _spawnPeriod = 10;

        private const float RADIUS_MULTIPLIER = 2.5f;
        private const float MAX_SPAWN_DELAY = 3f;
        private const float MIN_SPAWN_PERIOD = 0.25f;
        private const float TIME_REPEATING = 8;

        public event Action<EnemyEntity> OnEnemySpawned;


        [Inject]
        public void Construct(PlayerEntity playerEntity, DiContainer diContainer)
        {
            _playerEntity = playerEntity;
            _container = diContainer;
        }

        public void OnStartGame()
        {
            _camera = Camera.main;
            _player = _playerEntity.Get<TransformComponent>().EntityTransform;
            StartCoroutine(SpawnEnemyProcess());
        }

        public void OnLoseGame() => StopAllCoroutines();

        private IEnumerator SpawnEnemyProcess()
        {
            StartCoroutine(ProcessReduceSpawnPeriod());
            int randomValue;

            while (true)
            {
                randomValue = Random.Range(1, 4);

                for (int i = 0; i < randomValue; i++)
                {
                    StartCoroutine(StartSpawnEnemy());
                }

                yield return new WaitForSeconds(Random.Range(_spawnPeriod * 0.8f, _spawnPeriod * 1.2f));
            }
        }

        private IEnumerator StartSpawnEnemy()
        {
            float delay = Random.Range(0, MAX_SPAWN_DELAY);
            yield return new WaitForSeconds(delay);
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            Vector2 positionOnCircle = Random.insideUnitCircle.normalized;
            Vector3 relativeSpawnPos =
                new Vector3(positionOnCircle.x, 0, positionOnCircle.y) *
                _camera.orthographicSize *
                RADIUS_MULTIPLIER;

            Vector3 spawnPos = _player.position + relativeSpawnPos;

            EnemyEntity enemyEntity = _container.InstantiatePrefabForComponent<EnemyEntity>(
                _enemyPrefab,
                spawnPos,
                Quaternion.identity,
                _worldParent);

            OnEnemySpawned?.Invoke(enemyEntity);
        }

        private IEnumerator ProcessReduceSpawnPeriod()
        {
            yield return new WaitForSeconds(_spawnPeriod + 0.01f);

            while (_spawnPeriod > MIN_SPAWN_PERIOD)
            {
                ReduceSpawnPeriod();
                yield return new WaitForSeconds(TIME_REPEATING);
            }
        }

        private void ReduceSpawnPeriod()
        {
            float value = _spawnPeriod * 0.05f;
            _spawnPeriod -= value;
            _spawnPeriod = MathF.Max(_spawnPeriod, MIN_SPAWN_PERIOD);
            // Debug.Log("EnemySpawnPeriod = " + _spawnPeriod);
        }

        private void OnDrawGizmos()
        {
            Vector3 offset = Vector3.up * 0.001f;
            Vector3 drawPos = _player? _player.position + offset : offset;
            float radius = Camera.main.orthographicSize * RADIUS_MULTIPLIER;
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(drawPos, radius);
        }
    }
}