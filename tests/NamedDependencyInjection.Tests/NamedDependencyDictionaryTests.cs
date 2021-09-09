using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NamedDependencyInjection.Tests
{
    public class NamedDependencyDictionaryTests
    {
        [Theory]
        [InlineData(null, false, ServiceLifetime.Transient, typeof(ArgumentNullException))]
        [InlineData("", true, ServiceLifetime.Transient, typeof(ArgumentNullException))]
        [InlineData("key", true, ServiceLifetime.Transient, typeof(ArgumentNullException))]
        [InlineData("key", false, ServiceLifetime.Singleton, typeof(InvalidOperationException))]
        [InlineData("duplicated", false, ServiceLifetime.Transient, typeof(InvalidOperationException))]
        public void AddDependency_InvalidArguments_ThrowsAnException(string key, bool factoryIsNull, ServiceLifetime lifetime, Type exceptionType)
        {
            // Arrange
            Func<IServiceProvider, object> factory = factoryIsNull ? default(Func<IServiceProvider, object>) : sp => null;
            var target = Build();

            // Act
            Action act = () => target.AddDependency(key, factory, lifetime);

            // Assert
            Assert.Throws(exceptionType, act);
        }

        [Fact]
        public void AddDependency_ValidArguments_AddTheDependency()
        {
            // Arrange
            var target = Build();

            // Act
            target.AddDependency("key", sp => null, ServiceLifetime.Transient);

            // Assert
            target.ContainsKey("key").Should().BeTrue();
        }

        [Fact]
        public void Contructor_ShouldBeEmpty()
        {
            // Act
            var target = new NamedDependencyDictionary<object>();

            // Assert
            target.Should().BeEmpty();
        }

        private NamedDependencyDictionary<object> Build()
        {
            var target = new NamedDependencyDictionary<object>
            {
                ServiceLifetime = ServiceLifetime.Transient
            };
            target.Add("duplicated", sp => null);

            return target;
        }
    }
}
