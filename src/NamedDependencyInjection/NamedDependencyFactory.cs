using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NamedDependencyInjection
{
    internal class NamedDependencyFactory<TService>
    {
        private readonly NamedDependencyDictionary<TService> namedFactoryDictionary;
        private readonly IServiceProvider serviceProvider;
        private readonly ConcurrentDictionary<string, TService> container;

        public NamedDependencyFactory(IServiceProvider serviceProvider, NamedDependencyDictionary<TService> namedFactoryDictionary)
        {
            this.namedFactoryDictionary = namedFactoryDictionary;
            this.serviceProvider = serviceProvider;
            this.container = new ConcurrentDictionary<string, TService>();
        }

        public IEnumerable<TService> GetServices()
        {
            var services = new List<TService>();
            foreach (var item in this.namedFactoryDictionary)
            {
                services.Add(GetService(item.Key));
            }

            return services;
        }

        public IEnumerable<TService> GetRequiredServices()
        {
            if (!this.namedFactoryDictionary.Any())
            {
                throw new InvalidOperationException($"No service for type '{typeof(TService)}' has been registered with name.");
            }

            var services = new List<TService>();
            foreach (var item in this.namedFactoryDictionary)
            {
                services.Add(GetRequiredService(item.Key));
            }

            return services;
        }

        public TService GetService(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return container.GetOrAdd(name, ServiceFactory);
        }

        public TService GetRequiredService(string name)
        {
            var service = GetService(name);
            if (service == null)
            {
                throw new InvalidOperationException($"No service for type '{typeof(TService)}' has been registered with key '{name}'.");
            }

            return service;
        }

        private TService ServiceFactory(string name)
        {
            if (this.namedFactoryDictionary.TryGetValue(name, out var factory) && factory != null)
            {
                return factory.Invoke(this.serviceProvider);
            };

            return default;
        }
    }
}
