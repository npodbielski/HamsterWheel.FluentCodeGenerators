using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using System.Net;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

[SuppressMessage("Usage", "CA2263:Preferuj ogólne przeciążenie, gdy typ jest znany")]//this done for testing specific methods via specific tests
public class INamedSymbolExtensionsUnitTests
{
    [Fact]
    public void GetMemberFromInheritanceTree_WhenCalledWithString_ThenReturnsMemberFromBaseClass()
    {
        //arrange
        var expected = new DummyNamedTypeSymbol("member", "test");
        var someProp = new DummyNamedTypeSymbol("someProp", "test");
        var sut = new DummyNamedTypeSymbol("test", "test")
        {
            BaseType = new DummyNamedTypeSymbol("base", "test")
            {
                OtherMembers = [expected, someProp]
            }
        };

        //act
        var actual = sut.GetMemberFromInheritanceTree("member");

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GetMemberFromInheritanceTree_WhenCalledWithPredicate_ThenReturnsMemberFromBaseClass()
    {
        //arrange
        var member = new DummyNamedTypeSymbol("member", "test");
        var expected = new DummyNamedTypeSymbol("method", "test")
        {
            Kind = SymbolKind.Method
        };
        var sut = new DummyNamedTypeSymbol("test", "test")
        {
            BaseType = new DummyNamedTypeSymbol("base", "test")
            {
                OtherMembers = [member, expected]
            }
        };

        //act
        var actual = sut.GetMembersFromInheritanceTree(m => m.Kind == SymbolKind.Method);

        //assert
        actual.Should().BeEquivalentTo([expected]);
    }

    [Fact]
    public void IsType_WhenNameAndNamespaceMatch_ThenReturnsTrue()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var sut = new DummyNamedTypeSymbol("String", "System")
        {
            ContainingNamespace = systemNs
        };

        //act
        var actual = sut.IsType(typeof(string));

        //assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void IsType_WhenNameDiffers_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var sut = new DummyNamedTypeSymbol("Object", "System")
        {
            ContainingNamespace = systemNs
        };

        //act
        var actual = sut.IsType(typeof(string));

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void IsType_WhenNamespaceDiffers_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var collectionsNs = new DummyNamespaceSymbol("Collections") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("String", "System.Collections")
        {
            ContainingNamespace = collectionsNs
        };

        //act
        var actual = sut.IsType(typeof(string));

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void IsType_WhenBothNameAndNamespaceDiffers_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var collectionsNs = new DummyNamespaceSymbol("Collections") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("Object", "System.Collections")
        {
            ContainingNamespace = collectionsNs
        };

        //act
        var actual = sut.IsType(typeof(string));

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void IsType_WhenCalledWithCustomTypeAndMatch_ThenReturnsTrue()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var netNs = new DummyNamespaceSymbol("Net") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("IPAddress", "System.Net")
        {
            ContainingNamespace = netNs
        };

        //act
        var actual = sut.IsType(typeof(IPAddress));

        //assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void IsType_WhenCalledWithCustomTypeAndNoMatch_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var netNs = new DummyNamespaceSymbol("Net") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("IPAddress", "System.Net")
        {
            ContainingNamespace = netNs
        };

        //act
        var actual = sut.IsType(typeof(string));

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void IsTypeT_WhenNameAndNamespaceMatch_ThenReturnsTrue()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var sut = new DummyNamedTypeSymbol("String", "System")
        {
            ContainingNamespace = systemNs
        };

        //act
        var actual = sut.IsType<string>();

        //assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void IsTypeT_WhenNameDiffers_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var sut = new DummyNamedTypeSymbol("Object", "System")
        {
            ContainingNamespace = systemNs
        };

        //act
        var actual = sut.IsType<string>();

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void IsTypeT_WhenNamespaceDiffers_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var collectionsNs = new DummyNamespaceSymbol("Collections") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("String", "System.Collections")
        {
            ContainingNamespace = collectionsNs
        };

        //act
        var actual = sut.IsType<string>();

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void IsTypeT_WhenCalledWithCustomTypeAndMatch_ThenReturnsTrue()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var netNs = new DummyNamespaceSymbol("Net") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("IPAddress", "System.Net")
        {
            ContainingNamespace = netNs
        };

        //act
        var actual = sut.IsType<IPAddress>();

        //assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void IsTypeT_WhenCalledWithCustomTypeAndNoMatch_ThenReturnsFalse()
    {
        //arrange
        var systemNs = new DummyNamespaceSymbol("System");
        var netNs = new DummyNamespaceSymbol("Net") { Parent = systemNs };
        var sut = new DummyNamedTypeSymbol("IPAddress", "System.Net")
        {
            ContainingNamespace = netNs
        };

        //act
        var actual = sut.IsType<string>();

        //assert
        actual.Should().BeFalse();
    }
}