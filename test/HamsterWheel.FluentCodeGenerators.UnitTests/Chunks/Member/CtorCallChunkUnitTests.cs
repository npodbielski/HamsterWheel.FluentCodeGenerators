using System.Net;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Member;

public class CtorCallChunkUnitTests
{
    [Fact]
    public void AppendChunks_WhenWithNewPassed_ThenRendersNewKeyword()
    {
        //arrange
        var expected = "new IPAddress(1)";
        var sut = new CtorCallChunk(TypeNameChunk.From(typeof(IPAddress)),
            new ParametersValuesListChunk([new ParameterValueChunk(new PlainValueChunk("1"))]), true);

        //act
        var actual = sut.Build();

        //assert
        actual.Should().Be(expected);
    }
}