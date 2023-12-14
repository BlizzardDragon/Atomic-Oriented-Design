using System;
using System.Collections.Generic;
using System.Reflection;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;

namespace FrameworkUnity.OOP.Custom_DI.Internal
{
    public abstract class GameModule : MonoBehaviour
    {
        public virtual IEnumerable<object> GetServices()
        {
            Type type = GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(Service)))
                {
                    yield return field.GetValue(this);
                }
            }
        }

        public virtual IEnumerable<IGameListener> GetListeners()
        {
            Type type = GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(Listener)))
                {
                    object value = field.GetValue(this);
                    if (value is IGameListener gameListener)
                    {
                        yield return gameListener;
                    }
                }
            }
        }

        internal virtual void ResolveDependencies(GameSystem gameSystem)
        {
            Type type = GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (var field in fields)
            {
                object target = field.GetValue(this);
                gameSystem.Inject(target);
            }
        }
    }

    // Пример модуля.
    // public sealed class PlayeerModule : GameModule
    // {
    //     [Listener]
    //     [SerializeField]
    //     private CameraFollower _cameraFollower;
        
    //     [Service]
    //     [SerializeField]
    //     private PlayerService _playerService;
        
    //     [Listener]
    //     private MoveController _moveController = new();
        
    //     [Service, Listener]
    //     private KeyboardInput _keyboardInput = new();
    // }
}