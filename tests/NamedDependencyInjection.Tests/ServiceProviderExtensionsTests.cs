using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NamedDependencyInjection.Tests
{
    public class ServiceProviderExtensionsTests
    {
        [Theory]
        [InlineData(false, "service", typeof(ArgumentNullException))]
        [InlineData(true, null, typeof(ArgumentNullException))]
        [InlineData(true, "", typeof(ArgumentNullException))]
        public void GetNamedService_Parameters_ThrowsException(bool validProvider, string serviceName, Type exceptionType)
        {
            // Arrange
            var provider = Build(validProvider);

            // Act
            object act() => ServiceProviderExtensions.GetNamedService<object>(provider, serviceName);

            // Assert
            Assert.Throws(exceptionType, act);
        }

        [Theory]
        [InlineData("name", false)]
        [InlineData("noservice", true)]
        public void GetNamedService_Parameters_ShouldSameService(string serviceName, bool isNull)
        {
            // Arrange
            var provider = Build(true);

            // Act
            var result = ServiceProviderExtensions.GetNamedService<object>(provider, serviceName);

            // Assert
            (result == null).Should().Be(isNull);
        }

        [Theory]
        [InlineData(false, "service", typeof(ArgumentNullException))]
        [InlineData(true, null, typeof(ArgumentNullException))]
        [InlineData(true, "", typeof(ArgumentNullException))]
        [InlineData(true, "noservice", typeof(InvalidOperationException))]
        public void GetNamedRequiredService_Parameters_ThrowsException(bool validProvider, string serviceName, Type exceptionType)
        {
            // Arrange
            var provider = Build(validProvider);

            // Act
            object act() => ServiceProviderExtensions.GetNamedRequiredService<object>(provider, serviceName);

            // Assert
            Assert.Throws(exceptionType, act);
        }

        [Theory]
        [InlineData("name", false)]
        public void GetNamedRequiredService_Parameters_ShouldSameService(string serviceName, bool isNull)
        {
            // Arrange
            var provider = Build(true);

            // Act
            var result = ServiceProviderExtensions.GetNamedRequiredService<object>(provider, serviceName);

            // Assert
            (result == null).Should().Be(isNull);
        }

        [Theory]
        [InlineData(false, typeof(ArgumentNullException))]
        public void GetNamedServices_Parameters_ThrowsException(bool validProvider, Type exceptionType)
        {
            // Arrange
            var provider = Build(validProvider);

            // Act
            object act() => ServiceProviderExtensions.GetNamedServices<object>(provider);

            // Assert
            Assert.Throws(exceptionType, act);
        }

        [Fact]
        public void GetNamedServices_TypeObject_ShouldNotBeEmpty()
        {
            // Arrange
            var provider = Build(true);

            // Act
            var result = ServiceProviderExtensions.GetNamedServices<object>(provider);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetNamedServices_TypeString_ShouldBeNull()
        {
            // Arrange
            var provider = Build(true);

            // Act
            var result = ServiceProviderExtensions.GetNamedServices<string>(provider);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [InlineData(false, typeof(ArgumentNullException))]
        public void GetNamedRequiredServices_Parameters_ThrowsException(bool validProvider, Type exceptionType)
        {
            // Arrange
            var provider = Build(validProvider);

            // Act
            object act() => ServiceProviderExtensions.GetNamedRequiredServices<object>(provider);

            // Assert
            Assert.Throws(exceptionType, act);
        }

        [Fact]
        public void GetNamedRequiredServices_TypeObject_ShouldNotBeEmpty()
        {
            // Arrange
            var provider = Build(true);

            // Act
            var result = ServiceProviderExtensions.GetNamedRequiredServices<object>(provider);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetNamedRequiredServices_TypeString_ShouldBeNull()
        {
            // Arrange
            var provider = Build(true);

            // Act
            void act() => ServiceProviderExtensions.GetNamedRequiredServices<string>(provider);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        private IServiceProvider Build(bool validProvider)
        {
            if (!validProvider)
            {
                return null;
            }

            static object validFactory(IServiceProvider sp) => new object();

            var services = new ServiceCollection();
            services.AddNamed<object>("name", validFactory, ServiceLifetime.Transient);

            return services.BuildServiceProvider();
        }
    }
}
