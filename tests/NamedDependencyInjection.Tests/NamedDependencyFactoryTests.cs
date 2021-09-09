﻿using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace NamedDependencyInjection.Tests
{
    public class NamedDependencyFactoryTests
    {
        private const string ServiceName = "serviceName";
        private const string ServiceName2 = "serviceName2";
        private static readonly object Service = new object();
        private static readonly Func<IServiceProvider, object> ServiceFactory = sp => Service;

        private Mock<IServiceProvider> providerMock;
        private NamedDependencyDictionary<object> namedDependencyDictionary;

        [Fact]
        public void Constructor_Dependencies_VerifyNoCalls()
        {
            // Act
            var _ = Build();

            // Assert
            providerMock.VerifyNoOtherCalls();
            this.namedDependencyDictionary.Should().BeEmpty();
        }

        [Theory]
        [InlineData(ServiceName)]
        [InlineData(null)]
        public void GetService_NameIsNull_ThrowsArgumentNullException(string serviceName)
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: serviceName);

            // Act
            void act() => target.GetService(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void GetService_ServiceConfigurated_ShouldBeTheFactoryResult()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: ServiceName);

            // Act
            var result = target.GetService(ServiceName);

            // Assert
            result.Should().BeSameAs(Service);
        }

        [Fact]
        public void GetService_ServiceNotConfigured_ShoudlBeNull()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: null);

            // Act
            var result = target.GetService(ServiceName);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetServices_ServiceConfigurated_ShouldBeTheFactoryResult()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: ServiceName);
            AddNamedDependencyDictionary(serviceName: ServiceName2);

            // Act
            var result = target.GetServices();

            // Assert
            result.Should().HaveCount(2);
            foreach (var r in result)
            {
                r.Should().BeSameAs(Service);
            }
        }

        [Fact]
        public void GetServices_ServiceNotConfigured_ThrowException()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: null);

            // Act
            var result = target.GetServices();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(ServiceName)]
        [InlineData(null)]
        public void GetRequiredService_NameIsNull_ThrowsArgumentNullException(string serviceName)
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: serviceName);

            // Act
            void act() => target.GetRequiredService(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void GetRequiredService_ServiceConfigurated_ShouldBeTheFactoryResult()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: ServiceName);

            // Act
            var result = target.GetRequiredService(ServiceName);

            // Assert
            result.Should().BeSameAs(Service);
        }

        [Fact]
        public void GetRequiredService_ServiceNotConfigured_ThrowException()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: null);

            // Act
            void act() => target.GetRequiredService(ServiceName);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetRequiredServices_ServiceConfigurated_ShouldBeTheFactoryResult()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: ServiceName);
            AddNamedDependencyDictionary(serviceName: ServiceName2);

            // Act
            var result = target.GetRequiredServices();

            // Assert
            result.Should().HaveCount(2);
            foreach (var r in result)
            {
                r.Should().BeSameAs(Service);
            }
        }

        [Fact]
        public void GetRequiredServices_ServiceNotConfigured_ThrowException()
        {
            // Assert
            var target = Build();
            AddNamedDependencyDictionary(serviceName: null);

            // Act
            void act() => target.GetRequiredServices();

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        private void AddNamedDependencyDictionary(string serviceName)
        {
            if (serviceName != null)
            {
                this.namedDependencyDictionary.AddDependency(serviceName, ServiceFactory, ServiceLifetime.Transient);
            }
        }

        private NamedDependencyFactory<object> Build()
        {
            this.providerMock = new Mock<IServiceProvider>();
            this.namedDependencyDictionary = new NamedDependencyDictionary<object>
            {
                ServiceLifetime = ServiceLifetime.Transient
            };

            var target = new NamedDependencyFactory<object>(providerMock.Object, this.namedDependencyDictionary);

            return target;
        }
    }
}
