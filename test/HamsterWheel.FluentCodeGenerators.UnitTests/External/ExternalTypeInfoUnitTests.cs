using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.External;

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
    public void From_WhenCalled_ThenReturnsCorrectData()
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