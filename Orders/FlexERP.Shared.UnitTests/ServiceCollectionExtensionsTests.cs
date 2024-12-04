using System.Reflection;
using FlexERP.Shared.Abstractions;
using FlexERP.Shared.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace FlexERP.Shared.UnitTests;

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
    
    [Fact]
    public void AddRepositoriesByReflection_ShouldRegisterRepositoryImplementationsAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = CreateTestAssembly();

        // Act
        services.AddRepositoriesByReflection(assembly);
        var provider = services.BuildServiceProvider();

        // Assert
        var repo = provider.GetService<ITestRepository>();
        repo.Should().NotBeNull().And.BeOfType<TestRepository>();
        
        var concreteRepo = provider.GetService<TestRepository>();
        concreteRepo.Should().NotBeNull();
    }

    [Fact]
    public void AddRepositoriesByReflection_ShouldNotRegisterAbstractClasses()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = CreateTestAssembly();

        // Act
        services.AddRepositoriesByReflection(assembly);
        var provider = services.BuildServiceProvider();

        // Assert
        var abstractRepo = provider.GetService<AbstractRepository>();
        abstractRepo.Should().BeNull();
    }

    [Fact]
    public void AddRepositoriesByReflection_ShouldNotRegisterNonRepositoryClasses()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = CreateTestAssembly();

        // Act
        services.AddRepositoriesByReflection(assembly);
        var provider = services.BuildServiceProvider();

        // Assert
        var nonRepo = provider.GetService<NonRepository>();
        nonRepo.Should().BeNull();
    }

    private static Assembly CreateTestAssembly()
    {
        return typeof(ServiceCollectionExtensionsTests).Assembly;
    }
    
    internal interface ITestService : IService { }
    internal class TestService : ITestService { }
    internal class AnotherService : IService { }
    internal abstract class AbstractService : IService { }
    internal interface INonService { }
    internal class NonService : INonService { }
    
    private interface IRepository { }
    private interface ITestRepository : IRepository { }
    private class TestRepository : ITestRepository { }
    private abstract class AbstractRepository : IRepository { }
    private class NonRepository { }
}
