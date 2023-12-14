using System.Collections.Generic;
using FrameworkUnity.OOP.Custom_DI.Internal;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace FrameworkUnity.OOP.Custom_DI.Installers
{
    [RequireComponent(typeof(GameSystem))]
    public class GameInstaller : MonoBehaviour
    {
        [Header("AutoRun")]
        [SerializeField] private bool _initGame = true;
        [SerializeField] private bool _prepareGame = true;
        [SerializeField] private bool _startGame = true;

        [Header("GameModules")]
        [SerializeField] private GameModule[] _modules;

        protected GameSystem _gameSystem;


        protected virtual void Awake()
        {
            InstallGameSystem();
            InstallServices();
            InstallListerers();
            ResolveDependencies();

            if (_initGame)
            {
                _gameSystem.InitGame();
            }
        }

        private void InstallGameSystem()
        {
            _gameSystem = GetComponent<GameSystem>();
            _gameSystem.AddService(_gameSystem);
        }

        public void InstallServices()
        {
            foreach (var modul in _modules)
            {
                IEnumerable<object> services = modul.GetServices();
                _gameSystem.AddServices(services);
            }
        }

        private void InstallListerers()
        {
            foreach (var module in _modules)
            {
                IEnumerable<IGameListener> listeners = module.GetListeners();
                _gameSystem.AddListeners(listeners);
            }
        }

        public void ResolveDependencies()
        {
            foreach (var module in _modules)
            {
                module.ResolveDependencies(_gameSystem);
            }
        }

        protected virtual void Start()
        {
            if (_prepareGame)
            {
                _gameSystem.PrepareForGame();
            }

            if (_startGame)
            {
                _gameSystem.StartGame();
            }
        }

        protected virtual void OnDestroy() => _gameSystem.ClearServices();
    }
}