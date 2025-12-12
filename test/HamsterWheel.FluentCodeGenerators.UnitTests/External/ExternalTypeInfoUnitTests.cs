using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.External;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.External;

public class ExternalTypeInfoUnitTests
{
    [Fact]
    public void FromOfT1_WhenCalled_ThenReturnsCorrectData()
    {
        //arrange
        //act
        var actual = ExternalTypeInfo.From<int>();

        //assert
        actual.Name.NameAsString.Should().Be(typeof(int).Name);
        actual.Namespace.NamespaceAsString.Should().Be(typeof(int).Namespace);
        actual.NumberOfGenericArgs.Should().Be(0);
        actual.Attributes.Should().HaveCount(3);
        actual.IsArray.Should().BeFalse();
    }

    [Fact]
    public void From_WhenCalledWithType_ThenReturnsCorrectData()
    {
        //arrange
        //act
        var actual = ExternalTypeInfo.From(typeof(int));

        //assert
        actual.Name.NameAsString.Should().Be(typeof(int).Name);
        actual.Namespace.NamespaceAsString.Should().Be(typeof(int).Namespace);
        actual.NumberOfGenericArgs.Should().Be(0);
        actual.Attributes.Should().HaveCount(3);
        actual.IsArray.Should().BeFalse();
    }

    [Fact]
    public void From_WhenCalledWithSymbol_ThenReturnsCorrectData()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("test", "test")
        {
            AttributeData = [new DummyAttributeData("attr")]
        };

        //act
        var actual = ExternalTypeInfo.From(symbol);

        //assert
        actual.Name.NameAsString.Should().Be(symbol.Name.ToPascalCase());
        actual.Namespace.NamespaceAsString.Should().Be(symbol.ContainingNamespace.ToString());
        actual.NumberOfGenericArgs.Should().Be(0);
        actual.Attributes.Should().HaveCount(1);
        actual.IsArray.Should().BeFalse();
    }

    [Fact]
    public void From_WhenCalledWithSymbolWithAttributeWithoutTheClass_ThenNoAttributes()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("test", "test")
        {
            AttributeData = [new DummyAttributeData("attr"){ Class = null}]
        };

        //act
        var actual = ExternalTypeInfo.From(symbol);

        //assert
        actual.Attributes.Should().HaveCount(0);
    }

#pragma warning disable CA2263 // point of test
    [Fact]
    public void ToString_WhenCalled_ThenReturnsName()
    {
        //arrange
        var expected = typeof(int).Name;

        //act
        var actual = ExternalTypeInfo.From(typeof(int));

        //assert
        actual.ToString().Should().Be(expected);
    }
}