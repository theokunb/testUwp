using System.Collections.Generic;
using testUwp.Services;

namespace testUwp.Core
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance
        {
            get => _instance ?? (_instance = new ServiceLocator());
        }

        private Dictionary<string, IService> _services;

        private ServiceLocator()
        {
            _services = new Dictionary<string, IService>();
        }

        public void Register<T>(T service) where T : IService
        {
            string name = typeof(T).Name;

            if (_services.ContainsKey(name))
            {
                return;
            }

            _services.Add(name, service);
        }

        public void Unregister<T>() where T : IService
        {
            string name = typeof(T).Name;

            if (_services.ContainsKey(name))
            {
                _services.Remove(name);
            }
        }

        public T Get<T>() where T : IService
        {
            string name = typeof(T).Name;

            if (_services.ContainsKey(name))
            {
                return (T)_services[name];
            }

            return default(T);
        }
    }
}
