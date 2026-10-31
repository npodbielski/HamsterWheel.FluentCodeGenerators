using System.Net;
using System.Reflection;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using Microsoft.CodeAnalysis;
using HamsterWheel.FluentGenerators.AspNet;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.AspNet.Extensions;

public class IMethodSymbolExtensionsUnitTests
{
    [Fact]
    public void GetProducesResponseTypeAttributes_WhenCalledOnSymbolWithAttribute_ThenReturnsAttributes()
    {
        //arrange
        AttributeData[] expected = [new DummyAttributeData("ProducesResponseTypeAttribute")];
        var methodSymbol = new DummyMethodSymbol("GetCustomers", expected);

        //act
        var actual = methodSymbol.GetProducesResponseTypeAttributes();

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetProducesResponseTypeAttributes_WhenCalledOnSymbolWithoutAttribute_ThenReturnsEmpty()
    {
        //arrange
        var methodSymbol = new DummyMethodSymbol("GetCustomers");

        //act
        var actual = methodSymbol.GetProducesResponseTypeAttributes();

        //assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void GetProducesResponseTypeAttributes_WhenCalledOnSymbolWithOtherAttribute_ThenReturnsEmpty()
    {
        //arrange
        AttributeData[] expected = [new DummyAttributeData("HttpMethod")];
        var methodSymbol = new DummyMethodSymbol("GetCustomers", expected);

        //act
        var actual = methodSymbol.GetProducesResponseTypeAttributes();

        //assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void GetResponseFor200Ok_WhenCalledOnSymbolWithAttribute_ThenReturnsAttributes()
    {
        //arrange
        var typedConstantCtor = typeof(TypedConstant)
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .First(c => c.GetParameters().Length == 3);
        var dummyNamedTypeSymbol = new DummyNamedTypeSymbol("t", "t");
        AttributeData[] attributeData =
        [
            new DummyAttributeData("ProducesResponseTypeAttribute", [
                    (TypedConstant)typedConstantCtor.Invoke((object[])
                    [
                        null!,
                        TypedConstantKind.Enum,
                        (int)HttpStatusCode.OK
                    ])
                ], new Dictionary<string, TypedConstant>
                {
                    {
                        "test",
                        (TypedConstant)typedConstantCtor.Invoke((object[])
                        [
                            null!,
                            TypedConstantKind.Primitive,
                            dummyNamedTypeSymbol
                        ])
                    }
                }
            )
        ];
        var methodSymbol = new DummyMethodSymbol("GetCustomers", attributeData);

        //act
        var actual = methodSymbol.GetResponseFor200Ok();

        //assert
        actual.Should().BeEquivalentTo(dummyNamedTypeSymbol);
    }

    [Fact]
    public void GetResponseFor200Ok_WhenCalledOnSymbolWithout200Attribute_ThenReturnsNull()
    {
        //arrange
        var methodSymbol = new DummyMethodSymbol("GetCustomers");

        //act
        var actual = methodSymbol.GetResponseFor200Ok();

        //assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetResponseFor201Created_WhenCalledOnSymbolWithAttribute_ThenReturnsAttributes()
    {
        //arrange
        var typedConstantCtor = typeof(TypedConstant)
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .First(c => c.GetParameters().Length == 3);
        var dummyNamedTypeSymbol = new DummyNamedTypeSymbol("t", "t");
        AttributeData[] attributeData =
        [
            new DummyAttributeData("ProducesResponseTypeAttribute", [
                    (TypedConstant)typedConstantCtor.Invoke((object[])
                    [
                        null!,
                        TypedConstantKind.Enum,
                        (int)HttpStatusCode.Created
                    ])
                ], new Dictionary<string, TypedConstant>
                {
                    {
                        "test",
                        (TypedConstant)typedConstantCtor.Invoke((object[])
                        [
                            null!,
                            TypedConstantKind.Primitive,
                            dummyNamedTypeSymbol
                        ])
                    }
                }
            )
        ];
        var methodSymbol = new DummyMethodSymbol("GetCustomers", attributeData);

        //act
        var actual = methodSymbol.GetResponseFor201Created();

        //assert
        actual.Should().BeEquivalentTo(dummyNamedTypeSymbol);
    }

    [Fact]
    public void GetResponseFor201Created_WhenCalledOnSymbolWithout201Attribute_ThenReturnsNull()
    {
        //arrange
        var methodSymbol = new DummyMethodSymbol("GetCustomers");

        //act
        var actual = methodSymbol.GetResponseFor201Created();

        //assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Have307Response_WhenCalledOnSymbolWith307Attribute_ThenReturnsTrue()
    {
        //arrange
        var typedConstantCtor = typeof(TypedConstant)
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .First(c => c.GetParameters().Length == 3);
        AttributeData[] attributeData =
        [
            new DummyAttributeData("ProducesResponseTypeAttribute", [
                    (TypedConstant)typedConstantCtor.Invoke((object[])
                    [
                        null!,
                        TypedConstantKind.Enum,
                        (int)HttpStatusCode.TemporaryRedirect
                    ])
                ], new Dictionary<string, TypedConstant>()
            )
        ];
        var methodSymbol = new DummyMethodSymbol("GetCustomers", attributeData);

        //act
        var actual = methodSymbol.Have307Response();

        //assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Have307Response_WhenCalledOnSymbolWithout307Attribute_ThenReturnsFalse()
    {
        //arrange
        var methodSymbol = new DummyMethodSymbol("GetCustomers");

        //act
        var actual = methodSymbol.Have307Response();

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Have307Response_WhenCalledOnSymbolWithOtherStatusCode_ThenReturnsFalse()
    {
        //arrange
        var typedConstantCtor = typeof(TypedConstant)
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .First(c => c.GetParameters().Length == 3);
        AttributeData[] attributeData =
        [
            new DummyAttributeData("ProducesResponseTypeAttribute", [
                    (TypedConstant)typedConstantCtor.Invoke((object[])
                    [
                        null!,
                        TypedConstantKind.Enum,
                        (int)HttpStatusCode.OK
                    ])
                ], new Dictionary<string, TypedConstant>()
            )
        ];
        var methodSymbol = new DummyMethodSymbol("GetCustomers", attributeData);

        //act
        var actual = methodSymbol.Have307Response();

        //assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void GetHttpGetAttr_WhenCalledOnSymbolWithHttpGetAttribute_ThenReturnsAttribute()
    {
        //arrange
        var httpGetAttr = new DummyAttributeData("HttpGetAttribute");
        var methodSymbol = new DummyMethodSymbol("GetCustomers", [httpGetAttr]);

        //act
        var actual = methodSymbol.GetHttpGetAttr();

        //assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(httpGetAttr);
    }

    [Fact]
    public void GetHttpGetAttr_WhenCalledOnSymbolWithoutHttpGetAttribute_ThenReturnsNull()
    {
        //arrange
        var methodSymbol = new DummyMethodSymbol("GetCustomers");

        //act
        var actual = methodSymbol.GetHttpGetAttr();

        //assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetHttpGetAttr_WhenCalledOnSymbolWithOtherAttribute_ThenReturnsNull()
    {
        //arrange
        var httpMethodAttr = new DummyAttributeData("HttpMethod");
        var methodSymbol = new DummyMethodSymbol("GetCustomers", [httpMethodAttr]);

        //act
        var actual = methodSymbol.GetHttpGetAttr();

        //assert
        actual.Should().BeNull();
    }
}