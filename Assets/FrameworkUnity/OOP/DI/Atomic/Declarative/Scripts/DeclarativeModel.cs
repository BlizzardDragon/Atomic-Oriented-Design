using System;
using System.Collections.Generic;
using UnityEngine;

namespace Declarative
{
    [DefaultExecutionOrder(-1000)]
    public abstract class DeclarativeModel : MonoBehaviour
    {
        public Action onAwake;
        public Action onEnable;
        public Action onStart;
        public Action<float> onUpdate;
        public Action<float> onFixedUpdate;
        public Action<float> onLateUpdate;
        public Action onDisable;
        public Action onDestroy;

        [SerializeField]
        private MonoBehaviour[] externalSections;

        private Dictionary<Type, object> sections;
        private List<IDisposable> disposables;
        
        private MonoContext monoContext;
        
        internal bool TryGetSection(Type type, out object section)
        {
            return this.sections.TryGetValue(type, out section);
        }

        private void Awake()
        {
            this.monoContext = new MonoContext(this);
            this.disposables = new List<IDisposable>();
            this.sections = SectionScanner.ScanSections(this);
            
            foreach (var section in this.sections.Values)
            {
                SectionInitializer.InitSection(section, this);
            }

            foreach (var section in this.externalSections)
            {
                SectionInitializer.InitSection(section, this);
            }
            
            foreach (var section in this.sections.Values)
            {
                ElementRegisterer.RegisterElements(section, this.monoContext, this.disposables);
            }

            foreach (var section in this.externalSections)
            {
                ElementRegisterer.RegisterElements(section, this.monoContext, this.disposables);
            }

            this.monoContext.Awake();
            this.onAwake?.Invoke();
        }

        protected virtual void OnEnable()
        {
            this.monoContext.OnEnable();
            this.onEnable?.Invoke();
        }

        private void Start()
        {
            this.monoContext.Start();
            this.onStart?.Invoke();
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            this.monoContext.FixedUpdate(deltaTime);
            this.onFixedUpdate?.Invoke(deltaTime);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            this.monoContext.Update(deltaTime);
            this.onUpdate?.Invoke(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            this.monoContext.LateUpdate(deltaTime);
            this.onLateUpdate?.Invoke(deltaTime);
        }

        protected virtual void OnDisable()
        {
            this.monoContext?.OnDisable();
            this.onDisable?.Invoke();
        }

        private void OnDestroy()
        {
            this.monoContext?.OnDestroy();
            this.onDestroy?.Invoke();
            this.Dispose();
        }

        protected virtual void Dispose()
        {
            if (this.disposables != null)
            {
                foreach (var disposable in this.disposables)
                {
                    disposable.Dispose();
                }
            }

            DelegateUtils.Dispose(ref this.onAwake);
            DelegateUtils.Dispose(ref this.onEnable);
            DelegateUtils.Dispose(ref this.onStart);
            DelegateUtils.Dispose(ref this.onUpdate);
            DelegateUtils.Dispose(ref this.onFixedUpdate);
            DelegateUtils.Dispose(ref this.onLateUpdate);
            DelegateUtils.Dispose(ref this.onDisable);
            DelegateUtils.Dispose(ref this.onDestroy);
        }

#if UNITY_EDITOR
        [ContextMenu("Construct")]
        private void Construct()
        {
            this.Awake();
            this.OnEnable();
            Debug.Log($"<color=#FF6235>: {this.name} successfully constructed!</color>");
        }

        [ContextMenu("Destruct")]
        private void Destruct()
        {
            this.OnDisable();
            this.OnDestroy();
            Debug.Log($"<color=#FF6235>: {this.name} successfully destructed!</color>");
        }
#endif
    }
}