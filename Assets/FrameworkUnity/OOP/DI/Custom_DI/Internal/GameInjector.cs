using System;
using System.Reflection;

namespace FrameworkUnity.OOP.Custom_DI.Internal
{
    internal sealed class GameInjector
    {
        private readonly GameLocator _serviceLocator;

        public GameInjector(GameLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        internal void Inject(object target)
        {
            Type type = target.GetType();
            MethodInfo[] methodInfos = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.FlattenHierarchy
            );

            foreach (var method in methodInfos)
            {
                if (method.IsDefined(typeof(InjectAttribute)))
                {
                    InvokeMethod(method, target);
                }
            }
        }

        private void InvokeMethod(MethodInfo method, object target)
        {
            ParameterInfo[] parameterInfos = method.GetParameters();
            object[] args = new object[parameterInfos.Length];

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                ParameterInfo parameterInfo = parameterInfos[i];
                Type type = parameterInfo.ParameterType;
                object arg = _serviceLocator.GetService(type);
                args[i] = arg;
            }

            method.Invoke(target, args);
        }
    }
}
