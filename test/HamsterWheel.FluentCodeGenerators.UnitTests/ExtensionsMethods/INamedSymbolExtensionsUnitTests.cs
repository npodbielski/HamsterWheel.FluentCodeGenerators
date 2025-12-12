using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

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
}