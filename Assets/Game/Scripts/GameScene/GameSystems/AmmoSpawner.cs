using System;
using System.Collections;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AtomicOrientedDesign.Shooter
{
    public class AmmoSpawner : MonoBehaviour, IStartGameListener, ILoseGameListener
    {
        [SerializeField] private GameObject _ammoPrefab;
        [SerializeField] private Transform _worldParent;

        private Transform _player;
        private PlayerEntity _playerEntity;
        private Camera _camera;
        private DiContainer _container;
        [SerializeField] private Color _gizmosColor = Color.yellow;

        private float _spawnPeriod = 5;

        private const float RADIUS_MULTIPLIER = 2.5f;
        private const float MAX_SPAWN_DELAY = 3f;
        private const float MIN_SPAWN_PERIOD = 0.25f;
        private const float TIME_REPEATING = 8;
        private const float MIN_MULTIPLIER = 0.8f;
        private const float MAX_MULTIPLIER = 1.2f;
        private const int SPAWN_NUMBER = 1;


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
            StartCoroutine(SpawnAmmoProcess());
        }

        public void OnLoseGame() => StopAllCoroutines();

        private IEnumerator SpawnAmmoProcess()
        {
            StartCoroutine(ProcessReduceSpawnPeriod());
            int randomValue;

            while (true)
            {
                randomValue = Random.Range(1, SPAWN_NUMBER + 1);

                for (int i = 0; i < randomValue; i++)
                {
                    InvokeSpawnAmmo();
                }

                yield return new WaitForSeconds(Random.Range(_spawnPeriod * 0.8f, _spawnPeriod * 1.2f));
            }
        }

        private void InvokeSpawnAmmo()
        {
            float delay = Random.Range(0, MAX_SPAWN_DELAY);
            Invoke(nameof(SpawnAmmo), delay);
        }

        private void SpawnAmmo()
        {
            Vector2 positionOnCircle = Random.insideUnitCircle.normalized;
            Vector3 relativeSpawnPos =
                new Vector3(positionOnCircle.x, 0, positionOnCircle.y) *
                _camera.orthographicSize *
                RADIUS_MULTIPLIER * Random.Range(MIN_MULTIPLIER, MAX_MULTIPLIER);

            Vector3 spawnPos = _player.position + relativeSpawnPos;

            _container.InstantiatePrefabForComponent<AmmoEntity>(
                _ammoPrefab,
                spawnPos,
                Quaternion.identity,
                _worldParent);
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
            // Debug.Log("AmmoSpawnPeriod = " + _spawnPeriod);
        }

        private void OnDrawGizmos()
        {
            DrawSphere(MIN_MULTIPLIER);
            DrawSphere(MAX_MULTIPLIER);
        }

        private void DrawSphere(float multiplier)
        {
            Vector3 offset = Vector3.up * 0.001f;
            Vector3 drawPos = _player ? _player.position + offset : offset;
            float radius = Camera.main.orthographicSize * RADIUS_MULTIPLIER * multiplier;
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(drawPos, radius);
        }
    }
}