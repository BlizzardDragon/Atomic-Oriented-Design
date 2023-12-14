using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameMachines
{
    public sealed class GameMachine : MonoBehaviour
    {
        private readonly List<ITickable> tickables = new();
        private readonly List<IFixedTickable> fixedTickables = new();
        private readonly List<ILateTickable> lateTickables = new();

        private DiContainer container;

        [SerializeField]
        private bool autoRun = true;

        private void Start()
        {
            if (this.autoRun)
            {
                this.Initialize();
                this.Play();
            }
            else
            {
                this.enabled = false;
            }
        }

        private void Update()
        {
            for (int i = 0, count = this.tickables.Count; i < count; i++)
            {
                var tickable = this.tickables[i];
                tickable.Tick();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0, count = this.fixedTickables.Count; i < count; i++)
            {
                var tickable = this.fixedTickables[i];
                tickable.FixedTick();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0, count = this.lateTickables.Count; i < count; i++)
            {
                var tickable = this.lateTickables[i];
                tickable.LateTick();
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in this.container.Resolve<IEnumerable<IDisposable>>())
            {
                disposable.Dispose();
            }
        }

        [Button, PropertySpace]
        public void Initialize()
        {
            //Make dependency injection
            var sceneContext = FindObjectOfType<SceneContext>();
            sceneContext.Run();
            this.container = sceneContext.Container;

            //Load tickables:
            this.tickables.AddRange(this.container.Resolve<IEnumerable<ITickable>>());
            this.fixedTickables.AddRange(this.container.Resolve<IEnumerable<IFixedTickable>>());
            this.lateTickables.AddRange(this.container.Resolve<IEnumerable<ILateTickable>>());

            //Initialize components;
            foreach (var initializable in this.container.Resolve<IEnumerable<IInitializable>>())
            {
                initializable.Initialize();
            }
        }

        [Button]
        public void Play()
        {
            foreach (var listener in this.container.Resolve<IEnumerable<IGameStartListener>>())
            {
                listener.OnGameStart();
            }

            this.enabled = true;
        }

        [Button]
        public void Pause()
        {
            foreach (var listener in this.container.Resolve<IEnumerable<IGamePauseListener>>())
            {
                listener.OnGamePause();
            }

            this.enabled = false;
        }

        [Button]
        public void Resume()
        {
            foreach (var listener in this.container.Resolve<IEnumerable<IGameResumeListener>>())
            {
                listener.OnGameResume();
            }

            this.enabled = true;
        }

        [Button]
        public void Stop()
        {
            foreach (var listener in this.container.Resolve<IEnumerable<IGameStopListener>>())
            {
                listener.OnGameStop();
            }

            this.enabled = false;
        }
    }
}