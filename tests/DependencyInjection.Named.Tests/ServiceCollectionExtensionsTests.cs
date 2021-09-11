using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Gnet.Extensions.DependencyInjection.Named.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        private const Func<IServiceProvider, object> invalidFactory = null;
        private const IServiceCollection invalidProvider = null;

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentNullException))]
        [InlineData(true, "name", false, typeof(ArgumentNullException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamedScoped_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Scoped);

            // Act
            void act() => ServiceCollectionExtensions.AddNamedScoped(services, name, factory);

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, "name", false, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamedScoped_NamedServiceDictionary_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Scoped);

            // Act
            void act() => ServiceCollectionExtensions.AddNamedScoped(services, new NamedServiceDictionary<object> { { name, factory } });

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentNullException))]
        [InlineData(true, "name", false, typeof(ArgumentNullException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamedTransient_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Transient);

            // Act
            void act() => ServiceCollectionExtensions.AddNamedTransient(services, name, factory);

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, "name", false, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamedTransient_NamedServiceDictionary_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Transient);

            // Act
            void act() => ServiceCollectionExtensions.AddNamedTransient(services, new NamedServiceDictionary<object> { { name, factory } });

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentNullException))]
        [InlineData(true, "name", false, typeof(ArgumentNullException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamedSingleton_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Singleton);

            // Act
            void act() => ServiceCollectionExtensions.AddNamedSingleton(services, name, factory);

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, "name", false, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamedSingleton_NamedServiceDictionary_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Singleton);

            // Act
            void act() => ServiceCollectionExtensions.AddNamedSingleton(services, new NamedServiceDictionary<object> { { name, factory } });

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentNullException))]
        [InlineData(true, "name", false, typeof(ArgumentNullException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamed_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Transient);

            // Act
            void act() => ServiceCollectionExtensions.AddNamed(services, name, factory, ServiceLifetime.Transient);

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Theory]
        [InlineData(false, "name", true, typeof(ArgumentNullException))]
        [InlineData(true, null, true, typeof(ArgumentNullException))]
        [InlineData(true, "", true, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, "name", false, typeof(ArgumentOutOfRangeException))]
        [InlineData(true, " ", true, null)]
        [InlineData(true, "name", true, null)]
        [InlineData(true, "duplicated", true, typeof(InvalidOperationException))]
        public void AddNamed_NamedServiceDictionary_Parameters_ExpectedTheExceptionOrNot(bool servicesIsValid, string name, bool factoryIsValid, Type exceptionType)
        {
            // Arranje
            var (services, factory) = GetArguments(servicesIsValid, factoryIsValid, ServiceLifetime.Transient);

            // Act
            void act() => ServiceCollectionExtensions.AddNamed(services, new NamedServiceDictionary<object> { { name, factory } }, ServiceLifetime.Transient);

            // Assert
            if (exceptionType == null)
            {
                act();
            }
            else
            {
                Assert.Throws(exceptionType, act);
            }
        }

        [Fact]
        public void AddNamed_DifferentServiceLifetime_ExpectedInvalidOperationExpcetion()
        {
            // Arranje
            var (services, factory) = GetArguments(true, true, ServiceLifetime.Transient);

            // Act
            void act() => ServiceCollectionExtensions.AddNamed(services, new NamedServiceDictionary<object> { { "name", factory } }, ServiceLifetime.Singleton);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        public (IServiceCollection, Func<IServiceProvider, object>) GetArguments(bool servicesIsValid, bool factoryIsValid, ServiceLifetime serviceLifetime)
        {
            static object validFactory(IServiceProvider sp) => new object();

            var services = new ServiceCollection();
            services.AddNamed<object>("duplicated", validFactory, serviceLifetime);

            return (
                servicesIsValid ? services : invalidProvider,
                factoryIsValid ? validFactory : invalidFactory
            );
        }
    }
}
