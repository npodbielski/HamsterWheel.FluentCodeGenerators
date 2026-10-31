using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class StringValueChunkUnitTests
{
    [Fact]
    public void Ctor_WhenCalledWithMultiLineString_ThenUsesTripleQuote()
    {
        //arrange
        var expected = "\"\"\"" + """

                                  first line
                                  second line

                                  """ + "\"\"\"";

        //act
        var actual = new StringValueChunk("first line\nsecond line");

        //assert
        actual.Build().Should().Be(expected);
    }
}