﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NamedDependencyInjection
{
    public sealed class NamedDependencyDictionary<TService> : Dictionary<string, Func<IServiceProvider, TService>>
    {
        internal ServiceLifetime ServiceLifetime { get; set; }

        internal void AddDependency(string key, Func<IServiceProvider, TService> value, ServiceLifetime lifetime)
        {
            if (lifetime != this.ServiceLifetime)
            {
                throw new InvalidOperationException($"Failed to register named dependency '{key}' of {typeof(TService).FullName} with different lifetimes: {lifetime} and {this.ServiceLifetime}.");
            }

            if (this.ContainsKey(key))
            {
                throw new InvalidOperationException($"Failed to register a duplicated named dependency '{key}' of {typeof(TService).FullName}.");
            }

            this.Add(key, value);
        }
    }
}