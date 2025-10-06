using FluentAssertions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class StringExtensionsUnitTests
{
    [Fact]
    public void Quote_WhenCalled_ThenReturnsQuotedString()
    {
        //arrange
        var expected = "\"t\"";

        //act
        var actual = "t".Quote();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void TripleQuote_WhenCalled_ThenReturnsQuotedString()
    {
        //arrange
        var expected = "\"\"\"\nt\n\"\"\"";

        //act
        var actual = "t".TripleQuote();

        //assert
        actual.Should().Be(expected);
    }
}