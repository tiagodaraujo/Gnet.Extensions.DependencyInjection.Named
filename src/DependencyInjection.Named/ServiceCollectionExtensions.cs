﻿using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Gnet.Extensions.DependencyInjection.Named
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNamedScoped<TService>(this IServiceCollection services, string name, Func<IServiceProvider, TService> factory)
        {
            ValidArguments(services, name, factory);

            return services.AddNamedScoped(new NamedServiceDictionary<TService> { { name, factory } });
        }

        public static IServiceCollection AddNamedScoped<TService>(this IServiceCollection services, NamedServiceDictionary<TService> namedFactories)
        {
            ValidArguments(services, namedFactories);

            return services.AddNamed(namedFactories, ServiceLifetime.Scoped);
        }

        public static IServiceCollection AddNamedTransient<TService>(this IServiceCollection services, string name, Func<IServiceProvider, TService> factory)
        {
            ValidArguments(services, name, factory);

            return services.AddNamedTransient(new NamedServiceDictionary<TService> { { name, factory } });
        }

        public static IServiceCollection AddNamedTransient<TService>(this IServiceCollection services, NamedServiceDictionary<TService> namedFactories)
        {
            ValidArguments(services, namedFactories);

            return services.AddNamed(namedFactories, ServiceLifetime.Transient);
        }

        public static IServiceCollection AddNamedSingleton<TService>(this IServiceCollection services, string name, Func<IServiceProvider, TService> factory)
        {
            ValidArguments(services, name, factory);

            return services.AddNamedSingleton(new NamedServiceDictionary<TService> { { name, factory } });
        }

        public static IServiceCollection AddNamedSingleton<TService>(this IServiceCollection services, NamedServiceDictionary<TService> namedFactories)
        {
            ValidArguments(services, namedFactories);

            return services.AddNamed(namedFactories, ServiceLifetime.Singleton);
        }

        public static IServiceCollection AddNamed<TService>(this IServiceCollection services, string name, Func<IServiceProvider, TService> factory, ServiceLifetime lifetime)
        {
            ValidArguments(services, name, factory);

            return services.AddNamed(new NamedServiceDictionary<TService> { { name, factory } }, lifetime);
        }

        public static IServiceCollection AddNamed<TService>(this IServiceCollection services, NamedServiceDictionary<TService> namedFactories, ServiceLifetime lifetime)
        {
            ValidArguments(services, namedFactories);

            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(NamedServiceDictionary<TService>));
            if (descriptor != null)
            {
                var namedDictionary = descriptor.ImplementationInstance as NamedServiceDictionary<TService>;
                foreach (var item in namedFactories)
                {
                    if (string.IsNullOrEmpty(item.Key))
                    {
                        throw new InvalidOperationException($"The key of named dependency injection {typeof(TService).FullName} can't be empty or null.");
                    }

                    namedDictionary.AddDependency(item.Key, item.Value, lifetime);
                }

                return services;
            }

            namedFactories.ServiceLifetime = lifetime;
            services.AddSingleton<NamedServiceDictionary<TService>>(namedFactories);
            services.Add(
                new ServiceDescriptor(
                    typeof(NamedServiceFactory<TService>),
                    sp => new NamedServiceFactory<TService>(sp, sp.GetRequiredService<NamedServiceDictionary<TService>>()),
                    lifetime));

            return services;
        }

        private static void ValidArguments<TService>(IServiceCollection services, string name, Func<IServiceProvider, TService> factory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
        }

        private static void ValidArguments<TService>(IServiceCollection services, NamedServiceDictionary<TService> namedFactories)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (namedFactories == null)
            {
                throw new ArgumentNullException(nameof(namedFactories));
            }

            if (!namedFactories.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(namedFactories), "Should has one or more elements.");
            }

            if (namedFactories.Any(x => string.IsNullOrEmpty(x.Key)))
            {
                throw new ArgumentOutOfRangeException(nameof(namedFactories), "Service name null or empty is not valid.");
            }

            if (namedFactories.Any(x => x.Value == null))
            {
                throw new ArgumentOutOfRangeException(nameof(namedFactories), "Service factory null is not valid.");
            }
        }
    }
}
