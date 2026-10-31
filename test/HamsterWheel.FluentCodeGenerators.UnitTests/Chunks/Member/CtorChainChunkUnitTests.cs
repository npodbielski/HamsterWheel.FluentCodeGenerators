using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Member;

public class BaseCtorChunkUnitTests
{
    [Fact]
    public void AppendChunks_WhenParametersPassed_ThenRendersThem()
    {
        //arrange
        var expected = "base(1)";
        var sut = new BaseCtorChunk(new ParametersValuesListChunk([new ParameterValueChunk(new PlainValueChunk("1"))]));

        //act
        var actual = sut.Build();

        //assert
        actual.Should().Be(expected);
    }
}