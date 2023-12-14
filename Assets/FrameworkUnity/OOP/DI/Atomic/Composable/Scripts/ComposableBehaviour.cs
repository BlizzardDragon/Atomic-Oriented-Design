using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace Composable
{
    [AddComponentMenu("Composable/Composable Behaviour")]
    public sealed class ComposableBehaviour : MonoBehaviour
    {
        private readonly List<IInitializable> initializables = new();
        private readonly List<IDisposable> disposables = new();
        private readonly List<ITickable> tickables = new();
        private readonly List<IFixedTickable> fixedTickables = new();
        private readonly List<ILateTickable> lateTickables = new();
        
        [SerializeField]
        private MonoBehaviour[] sections;

        private void Awake()
        {
            foreach (var section in this.sections)
            {
                this.ComposeSection(section);
            }
        }

        private void ComposeSection(object section)
        {
            var type = section.GetType();
            
            while (type != null && type != typeof(object) && type != typeof(MonoBehaviour))
            {
                var methods = type.GetMethods(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.DeclaredOnly
                );

                for (int i = 0, count = methods.Length; i < count; i++)
                {
                    var method = methods[i];
                    if (method.IsDefined(typeof(ComposeAttribute)))
                    {
                        var args = this.ResolveArgs(method);
                        method.Invoke(section, args);
                    }
                }
                
                type = type.BaseType;
            }
        }

        private object[] ResolveArgs(MethodBase method)
        {
            var parameters = method.GetParameters();
            var count = parameters.Length;

            var args = new object[count];
            for (var i = 0; i < count; i++)
            {
                var parameter = parameters[i];
                args[i] = this.ResolveArg(parameter.ParameterType);
            }

            return args;
        }

        private object ResolveArg(Type argType)
        {
            foreach (var section in this.sections)
            {
                if (section.GetType() == argType)
                {
                    return section;
                }
            }
            
            Debug.LogWarning($"Can't find arg of type: {argType.Name}");
            return null;
        }

        private void Start()
        {
            foreach (var section in this.sections)
            {
                this.AddElement(section);
                this.ScanElements(section);
            }
            
            foreach (var initializable in this.initializables)
            {
                initializable.Initialize();
            }
        }
        
        private void ScanElements(object section)
        {
            var type = section.GetType();
            
            while (type != null && type != typeof(object) && type != typeof(MonoBehaviour))
            {
                var fields = type.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.DeclaredOnly
                );

                for (int i = 0, count = fields.Length; i < count; i++)
                {
                    var field = fields[i];
                    var instance = field.GetValue(section);
                    this.AddElement(instance);
                }
                
                type = type.BaseType;
            }
        }

        private void AddElement(object target)
        {
            if (target is ITickable tickable)
            {
                this.tickables.Add(tickable);
            }

            if (target is IFixedTickable fixedTickable)
            {
                this.fixedTickables.Add(fixedTickable);
            }

            if (target is ILateTickable lateTickable)
            {
                this.lateTickables.Add(lateTickable);
            }

            if (target is IInitializable initializable)
            {
                this.initializables.Add(initializable);
            }

            if (target is IDisposable disposable)
            {
                this.disposables.Add(disposable);
            }
        }

        private void Update()
        {
            foreach (var tickable in this.tickables)
            {
                tickable.Tick();
            }
        }

        private void FixedUpdate()
        {
            foreach (var tickable in this.fixedTickables)
            {
                tickable.FixedTick();
            }
        }

        private void LateUpdate()
        {
            foreach (var tickable in this.lateTickables)
            {
                tickable.LateTick();
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in this.disposables)
            {
                disposable.Dispose();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Compose")]
        private void EditorCompose()
        {
            this.Awake();
            this.Start();
        }

        [ContextMenu("Decompose")]
        private void EditorDecompose()
        {
            this.OnDestroy();
        }
#endif
    }
}