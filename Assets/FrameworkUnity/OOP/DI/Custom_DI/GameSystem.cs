using System;
using System.Collections.Generic;
using FrameworkUnity.OOP.Custom_DI.Installers;
using FrameworkUnity.OOP.Custom_DI.Internal;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace FrameworkUnity.OOP.Custom_DI
{
    [RequireComponent(typeof(GameInstaller))]
    public class GameSystem : MonoBehaviour
    {
        public GameSystem()
        {
            _injector = new(_serviceLocator);
        }

        #region GameListeners

        [SerializeField] public GameState State => _gameMachine.State;
        private readonly GameMachine _gameMachine = new();

        private void Update() => _gameMachine.Update();
        private void FixedUpdate() => _gameMachine.FixedUpdate();
        private void LateUpdate() => _gameMachine.LateUpdate();

        public void AddListener(IGameListener listener) => _gameMachine.AddListener(listener);
        public void RemoveListener(IGameListener listener) => _gameMachine.RemoveListener(listener);
        internal void AddListeners(IEnumerable<IGameListener> listeners) => _gameMachine.AddListeners(listeners);

        [ContextMenu(nameof(InitGame))]
        public void InitGame() => _gameMachine.InitGame();

        [ContextMenu(nameof(PrepareForGame))]
        public void PrepareForGame() => _gameMachine.PrepareForGame();

        [ContextMenu(nameof(StartGame))]
        public void StartGame() => _gameMachine.StartGame();

        [ContextMenu(nameof(PauseGame))]
        public void PauseGame() => _gameMachine.PauseGame();

        [ContextMenu(nameof(ResumeGame))]
        public void ResumeGame() => _gameMachine.ResumeGame();

        [ContextMenu(nameof(FinishGame))]
        public void FinishGame() => _gameMachine.FinishGame();

        [ContextMenu(nameof(WinGame))]
        public void WinGame() => _gameMachine.WinGame();

        [ContextMenu(nameof(LoseGame))]
        public void LoseGame() => _gameMachine.LoseGame();

        #endregion

        #region ServiceLocator

        private readonly GameLocator _serviceLocator = new();

        public T GetService<T>() => _serviceLocator.GetService<T>();
        public object GetService(Type serviceType) => _serviceLocator.GetService(serviceType);
        public List<T> GetServices<T>() => _serviceLocator.GetServices<T>();
        public void AddService(object newService) => _serviceLocator.AddService(newService);
        internal void AddServices(IEnumerable<object> services) => _serviceLocator.AddServices(services);
        public void ClearServices() => _serviceLocator.ClearServices();

        #endregion

        #region DependencyInjector

        private readonly GameInjector _injector;
        public void Inject(object target) => _injector.Inject(target);

        #endregion
    }
}