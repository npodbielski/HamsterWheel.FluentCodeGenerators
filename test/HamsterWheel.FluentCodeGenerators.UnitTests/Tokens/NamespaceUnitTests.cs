using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Tokens;

public class NamespaceUnitTests
{
    [Fact]
    public void Append_WhenCalled_ThenAppendsSubNamespace()
    {
        //arrange
        var expected = "HamsterWheel.FluentCodeGenerators";
        var sut = new Namespace("HamsterWheel");

        //act
        var actual = sut.Append("FluentCodeGenerators");

        //assert
        actual.ToString().Should().Be(expected);
    }
}