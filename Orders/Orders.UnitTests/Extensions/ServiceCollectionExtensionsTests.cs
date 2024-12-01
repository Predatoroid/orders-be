using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Orders.Abstractions;
using Orders.Extensions;

namespace Orders.UnitTests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddServicesByReflection_ShouldRegisterServicesCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();

        // Act
        services.AddServicesByReflection(assembly);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        // Check that TestService is registered as ITestService
        var testService = serviceProvider.GetService<ITestService>();
        testService.Should().NotBeNull();
        testService.Should().BeOfType<TestService>();

        // Check that TestService is registered as itself
        var testServiceAsSelf = serviceProvider.GetService<TestService>();
        testServiceAsSelf.Should().NotBeNull();

        // Check that AnotherService is registered as itself
        var anotherService = serviceProvider.GetService<AnotherService>();
        anotherService.Should().NotBeNull();
    }

    [Fact]
    public void AddServicesByReflection_ShouldExcludeAbstractAndNonIServiceTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();

        // Act
        services.AddServicesByReflection(assembly);

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        // AbstractService should not be registered
        var abstractService = serviceProvider.GetService<AbstractService>();
        abstractService.Should().BeNull();

        // NonService should not be registered
        var nonService = serviceProvider.GetService<NonService>();
        nonService.Should().BeNull();
    }

    [Fact]
    public void AddServicesByReflection_ShouldHandleEmptyAssembly()
    {
        // Arrange
        var services = new ServiceCollection();
        var emptyAssemblyMock = new Mock<Assembly>();
        emptyAssemblyMock.Setup(a => a.GetTypes()).Returns(Array.Empty<Type>());

        // Act
        services.AddServicesByReflection(emptyAssemblyMock.Object);

        // Assert
        services.Should().BeEmpty();
    }

    [Fact]
    public void AddServicesByReflection_WhenNullAssemblyShouldThrowException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        Action act = () => services.AddServicesByReflection(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}


internal interface ITestService : IService { }
internal class TestService : ITestService { }
internal class AnotherService : IService { }
internal abstract class AbstractService : IService { }
internal interface INonService { }
internal class NonService : INonService { }