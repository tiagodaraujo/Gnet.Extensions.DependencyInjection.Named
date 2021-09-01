using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NamedDependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static TService GetNamedService<TService>(this IServiceProvider provider, string name)
        {
            ValidArguments(provider, name);
            var namedFactory = provider.GetService<NamedDependencyFactory<TService>>();

            return namedFactory != null ? namedFactory.GetService(name) : default;
        }

        public static TService GetNamedRequiredService<TService>(this IServiceProvider provider, string name)
        {
            ValidArguments(provider, name);
            return provider.GetRequiredService<NamedDependencyFactory<TService>>().GetRequiredService(name);
        }

        public static IEnumerable<TService> GetNamedServices<TService>(this IServiceProvider provider)
        {
            ValidArguments(provider);
            var service = provider.GetService<NamedDependencyFactory<TService>>();

            return service != null ? service.GetServices() : default;
        }

        public static IEnumerable<TService> GetNamedRequiredServices<TService>(this IServiceProvider provider)
        {
            ValidArguments(provider);

            return provider.GetRequiredService<NamedDependencyFactory<TService>>().GetRequiredServices();
        }

        private static void ValidArguments(IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
        }

        private static void ValidArguments(IServiceProvider provider, string name)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
        }
    }
}
