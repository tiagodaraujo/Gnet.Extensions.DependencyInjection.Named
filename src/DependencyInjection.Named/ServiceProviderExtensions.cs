using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Gnet.Extensions.DependencyInjection.Named
{
    public static class ServiceProviderExtensions
    {
        public static TService GetNamedService<TService>(this IServiceProvider provider, string name)
        {
            ValidArguments(provider, name);
            var namedFactory = provider.GetService<NamedServiceFactory<TService>>();

            return namedFactory != null ? namedFactory.GetService(name) : default;
        }

        public static TService GetNamedRequiredService<TService>(this IServiceProvider provider, string name)
        {
            ValidArguments(provider, name);
            return provider.GetRequiredService<NamedServiceFactory<TService>>().GetRequiredService(name);
        }

        public static IEnumerable<TService> GetNamedServices<TService>(this IServiceProvider provider)
        {
            ValidArguments(provider);
            var service = provider.GetService<NamedServiceFactory<TService>>();

            return service != null ? service.GetServices() : default;
        }

        public static IEnumerable<TService> GetNamedRequiredServices<TService>(this IServiceProvider provider)
        {
            ValidArguments(provider);

            return provider.GetRequiredService<NamedServiceFactory<TService>>().GetRequiredServices();
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
