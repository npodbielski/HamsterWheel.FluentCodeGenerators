using System.Text;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class StringBuilderExtensionsUnitTests
{
    [Fact]
    public void AppendChunkOfT1_WhenCalledWithChunkInstance_ThenAddsThisChunk()
    {
        //arrange
        var expected = "nameof";
        var sb = new StringBuilder();

        //act
        var actual = sb.AppendChunk(new NameOfKeywordChunk());

        //assert
        actual.ToString().Should().Be(expected);
    }
}