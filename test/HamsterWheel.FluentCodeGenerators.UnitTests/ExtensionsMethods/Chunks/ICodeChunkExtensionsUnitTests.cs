using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks;

public class ICodeChunkExtensionsUnitTests
{
    [Fact]
    public void Build_WhenCalled_ThenRendersChunk()
    {
        //arrange
        var expected = "2";

        //act
        var actual = new PlainValueChunk("2").Build();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Build_WhenCalledWithMethodBodyChunks_ThenRendersChunk()
    {
        //arrange
        var expected = """
                       
                       {
                           return test;
                       }
                       """;

        //act
        var bodyChunks = new MethodBodyChunks();
        bodyChunks.Append("return test;");
        var actual = bodyChunks.Build();

        //assert
        actual.Should().Be(expected);
    }
}