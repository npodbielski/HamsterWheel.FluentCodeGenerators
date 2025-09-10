using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class TypeNameChunkUnitTests
{
    private readonly TypeNameChunk _chunk = new("string");

    [Fact]
    public void Name_WhenCalled_ThenReturnsCorrectValue()
    {
        //arrange
        var expected = "string";

        //act
        var actual = _chunk.Name;

        //assert
        actual.ToString().Should().Be(expected);
    }

    [Fact]
    public void Name_WhenCalledFromInterface_ThenReturnsCorrectValue()
    {
        //arrange
        var expected = "string";

        //act
        var actual = ((INamedChunk)_chunk).Name;

        //assert
        actual.ToString().Should().Be(expected);
    }
}