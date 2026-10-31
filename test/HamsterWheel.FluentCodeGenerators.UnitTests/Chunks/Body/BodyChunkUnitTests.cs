using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Body;

public class BodyChunkUnitTests
{
    [Fact]
    public void ToString_WhenCalled_ThenRendersChunk()
    {
        //arrange
        var expected = "nameof";

        //act
        var actual = new NameOfKeywordChunk().ToString();

        //assert
        actual.Should().Be(expected);
    }
}