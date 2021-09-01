using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
            foreach (var item in this.namedFactoryDictionary)
            {
                yield return GetService(item.Key);
            }
        }

        public IEnumerable<TService> GetRequiredServices()
        {
            foreach (var item in this.namedFactoryDictionary)
            {
                yield return GetRequiredService(item.Key);
            }
        }

        public TService GetService(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (container.TryGetValue(name, out var service))
            {
                return service;
            }

            return container.AddOrUpdate(name, ServiceFactory, (serviceName, oldService) => oldService);
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
            if (!this.namedFactoryDictionary.ContainsKey(name))
            {
                this.namedFactoryDictionary.TryGetValue(string.Empty, out var defaultFactory);

                return (defaultFactory ?? NullService).Invoke(this.serviceProvider);
            }

            this.namedFactoryDictionary.TryGetValue(name, out var factory);

            return (factory ?? NullService).Invoke(this.serviceProvider);
        }

        private static TService NullService(IServiceProvider sp) => default;
    }
}
