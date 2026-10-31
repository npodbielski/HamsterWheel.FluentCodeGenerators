using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Providers;

public class TypeResolverServiceUnitTests
{
    [Fact]
    public void GetExternalType_WhenCalledWithNameAndNamespace_ThenConvertsNameToPascalCaseAndAddsNamespace()
    {
        // arrange
        var expectedType = new DummyNamedTypeSymbol("MyType", "MyNamespace");
        string? calculatedFullName = null;

        var service = new TypeResolverService(Resolver);
        var namespaceName = new Namespace("MyNamespace");

        // act
        var result = service.GetExternalType("myType", namespaceName);

        // assert
        result.Should().Be(expectedType);
        calculatedFullName.Should().Be("MyNamespace.MyType");
        return;

        INamedTypeSymbol? Resolver(string fullName)
        {
            calculatedFullName = fullName;
            return expectedType;
        }
    }

    [Fact]
    public void GetExternalType_WhenCalledWithNameAndNamespace_ThenThrowsIfTypeNotFound()
    {
        // arrange
        var service = new TypeResolverService(Resolver);
        var namespaceName = new Namespace("MyNamespace");

        // act
        var action = () => service.GetExternalType("myType", namespaceName);

        // assert
        action.Should().Throw<NoExternalTypeFoundException>();
        return;

        INamedTypeSymbol? Resolver(string _) => null;
    }

    [Fact]
    public void GetExternalType_WhenCalledWithGenericParametersAndTypeDoesNotExist_ThenThrows()
    {
        // arrange
        var service = new TypeResolverService(Resolver);

        // act
        var action = () => service.GetExternalType("MyNamespace.MyGenericType", 2);

        // assert
        action.Should().Throw<NoExternalTypeFoundException>();
        return;

        INamedTypeSymbol? Resolver(string _) => null;
    }

    [Fact]
    public void GetExternalType_WhenCalledWithGenericParametersAndTypeExists_ThenReturnsType()
    {
        // arrange
        var expectedType = new DummyNamedTypeSymbol("MyGenericType", "MyNamespace");
        var service = new TypeResolverService(Resolver);

        // act
        var result = service.GetExternalType("MyNamespace.MyGenericType", 2);

        // assert
        result.Should().Be(expectedType);
        return;

        INamedTypeSymbol? Resolver(string _) => expectedType;
    }

    [Fact]
    public void GetExternalType_WhenTypeDoesNotExist_ThenThrowsNoExternalTypeFoundException()
    {
        // arrange
        var service = new TypeResolverService(Resolver);

        // act
        var action = () => service.GetExternalType("NonExistentType");

        // assert
        action.Should().Throw<NoExternalTypeFoundException>()
            .WithMessage("*NonExistentType*");
        return;

        INamedTypeSymbol? Resolver(string _) => null;
    }

    [Fact]
    public void GetExternalType_WhenWithNamespaceAndTypeExists_ThenReturnsType()
    {
        // arrange
        var expectedType = new DummyNamedTypeSymbol("MyType", "MyNamespace");
        var service = new TypeResolverService(Resolver);

        // act
        var result = service.GetExternalType("MyNamespace.MyType");

        // assert
        result.Should().Be(expectedType);
        return;
        INamedTypeSymbol? Resolver(string _) => expectedType;
    }

    [Fact]
    public void GetExternalTypeOrNull_WhenTypeExists_ThenReturnsType()
    {
        // arrange
        var expectedType = new DummyNamedTypeSymbol("MyType", "MyNamespace");
        var service = new TypeResolverService(Resolver);

        // act
        var result = service.GetExternalTypeOrNull("MyNamespace.MyType");

        // assert
        result.Should().Be(expectedType);
        return;

        INamedTypeSymbol? Resolver(string _) => expectedType;
    }

    [Fact]
    public void GetExternalTypeOrNull_WhenTypeDoesNotExist_ThenReturnsNull()
    {
        // arrange
        var service = new TypeResolverService(Resolver);

        // act
        var result = service.GetExternalTypeOrNull("NonExistentType");

        // assert
        result.Should().BeNull();
        return;

        INamedTypeSymbol? Resolver(string _) => null;
    }

    [Fact]
    public void GetExternalType_WhenCalledWithGenericParameters_ThenAppendsBacktickAndNumber()
    {
        // arrange
        var expectedType = new DummyNamedTypeSymbol("MyGenericType", "MyNamespace");
        string? calculatedFullName = null;

        var service = new TypeResolverService(Resolver);

        // act
        var result = service.GetExternalTypeOrNull("MyNamespace.MyGenericType", 2);

        // assert
        result.Should().Be(expectedType);
        calculatedFullName.Should().EndWith("`2");
        return;

        INamedTypeSymbol? Resolver(string fullName)
        {
            calculatedFullName = fullName;
            return expectedType;
        }
    }

    [Fact]
    public void GetExternalType_WhenCalledWithDefaultGenericParameters_ThenDoesNotAppendBacktick()
    {
        // arrange
        var expectedType = new DummyNamedTypeSymbol("MyType", "MyNamespace");

        var service = new TypeResolverService(Resolver);

        // act
        var result = service.GetExternalTypeOrNull("MyNamespace.MyType");

        // assert
        result.Should().Be(expectedType);
        return;

        INamedTypeSymbol? Resolver(string fullName)
        {
            fullName.Should().NotContain("`");
            return expectedType;
        }
    }
}