using System.Collections.Generic;
using UnityEngine;
using System;

namespace FrameworkUnity.OOP.Custom_DI.Internal
{
    internal sealed class GameLocator
    {
        private readonly List<object> _services = new List<object>();

        internal T GetService<T>()
        {
            foreach (var service in _services)
            {
                if (service is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Service of type {typeof(T).Name} is not found!");
        }

        internal object GetService(Type serviceType)
        {
            foreach (var service in _services)
            {
                if (service.GetType() == serviceType)
                {
                    return service;
                }
            }

            throw new Exception($"Service of type {serviceType.Name} is not found!");
        }

        internal List<T> GetServices<T>()
        {
            var result = new List<T>();
            foreach (var service in _services)
            {
                if (service is T tService)
                {
                    result.Add(tService);
                }
            }

            return result;
        }

        internal void AddService(object newService)
        {
            foreach (var service in _services)
            {
                if (service == newService)
                {
                    throw new Exception($"Service object of type {newService.GetType().Name} is already in the list!");
                }
                else
                {
                    if (service.GetType() == newService.GetType())
                    {
                        Debug.LogWarning($"Service of type {newService.GetType().Name} is already in the list!");
                    }
                }
            }

            _services.Add(newService);
        }
        
        internal void AddServices(IEnumerable<object> services) => _services.AddRange(services);

        internal void ClearServices() => _services.Clear();
    }
}
