using System;
using System.Collections.Generic;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace FrameworkUnity.OOP.Custom_DI.Internal
{
    internal sealed class GameMachine
    {
        internal GameState State { get; private set; }
        private float _fixedDeltaTime;

        private readonly List<IGameListener> _listeners = new();
        private readonly List<IUpdateGameListener> _updateListeners = new();
        private readonly List<IFixedUpdateGameListener> _fixedUpdateListeners = new();
        private readonly List<ILateUpdateGameListener> _lateUpdateListeners = new();

        internal event Action OnInitGame;
        internal event Action OnDeInitGame;
        internal event Action OnPrepareForGame;
        internal event Action OnStartGame;
        internal event Action OnPauseGame;
        internal event Action OnResumeGame;
        internal event Action OnFinishGame;
        internal event Action OnGameWin;
        internal event Action OnGameOver;


        internal void Update()
        {
            if (State != GameState.Playing) return;

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(deltaTime);
            }
        }

        internal void FixedUpdate()
        {
            if (State != GameState.Playing) return;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(_fixedDeltaTime);
            }
        }

        internal void LateUpdate()
        {
            if (State != GameState.Playing) return;

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }

        internal void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            _listeners.Add(listener);

            if (listener is IUpdateGameListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (listener is IFixedUpdateGameListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is ILateUpdateGameListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        internal void RemoveListener(IGameListener listener)
        {
            if (listener == null) return;

            _listeners.Remove(listener);

            if (listener is IUpdateGameListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }

            if (listener is IFixedUpdateGameListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is ILateUpdateGameListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        internal void AddListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (var listener in listeners)
            {
                AddListener(listener);
            }
        }

        internal void InitGame()
        {
            _fixedDeltaTime = Time.fixedDeltaTime;
            
            foreach (var listener in _listeners)
            {
                if (listener is IInitGameListener initListener)
                {
                    initListener.OnInitGame();
                }
            }

            OnInitGame?.Invoke();
        }

        internal void DeInitGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IDeInitGameListener deInitListener)
                {
                    deInitListener.OnDeInitGame();
                }
            }

            OnDeInitGame?.Invoke();
        }

        internal void PrepareForGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IPrepareGameListener prepareListener)
                {
                    prepareListener.OnPrepareGame();
                }
            }

            State = GameState.Preparing;
            OnPrepareForGame?.Invoke();
        }

        internal void StartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IStartGameListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            State = GameState.Playing;
            OnStartGame?.Invoke();
        }

        internal void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IPauseGameListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }

            State = GameState.Pause;
            OnPauseGame?.Invoke();
        }

        internal void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IResumeGameListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }

            State = GameState.Playing;
            OnResumeGame?.Invoke();
        }

        internal void WinGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IWinGameListener gameWinListener)
                {
                    gameWinListener.OnWinGame();
                }
            }

            DeInitGame();
            State = GameState.Win;
            OnGameWin?.Invoke();
        }

        internal void LoseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is ILoseGameListener gameOverListener)
                {
                    gameOverListener.OnLoseGame();
                }
            }

            DeInitGame();
            State = GameState.Lose;
            OnGameOver?.Invoke();
        }

        internal void FinishGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IFinishGameListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }

            DeInitGame();
            State = GameState.Finish;
            OnFinishGame?.Invoke();
        }
    }
}