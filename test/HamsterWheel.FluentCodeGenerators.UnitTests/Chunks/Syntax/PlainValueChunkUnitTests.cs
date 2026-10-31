using System.Globalization;
using System.Text;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class PlainValueChunkUnitTests
{
    [Fact]
    public void Build_WhenCalledWithStringValue_ThenReturnsValue()
    {
        //arrange
        const string expected = "test value";

        //act
        var actual = new PlainValueChunk(expected).Build();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Build_WhenCalledWithStringBuilderValue_ThenReturnsStringBuilderContent()
    {
        //arrange
        var stringBuilder = new StringBuilder("string builder value");

        //act
        var actual = new PlainValueChunk(stringBuilder).Build();

        //assert
        actual.Should().Be("string builder value");
    }

    [Fact]
    public void AppendChunks_WhenCalledWithStringValue_ThenAppendsValue()
    {
        //arrange
        var target = new StringBuilder();
        const string expected = "appended value";

        //act
        var result = new PlainValueChunk(expected).AppendChunks(target);

        //assert
        result.Should().BeTrue();
        target.ToString().Should().Be(expected);
    }

    [Fact]
    public void AppendChunks_WhenCalledWithStringBuilderValue_ThenAppendsStringBuilderContent()
    {
        //arrange
        var target = new StringBuilder();
        var source = new StringBuilder("source content");

        //act
        var result = new PlainValueChunk(source).AppendChunks(target);

        //assert
        result.Should().BeTrue();
        target.ToString().Should().Be("source content");
    }

    [Fact]
    public void FromObject_WhenCalledWithInt_ThenConvertsToString()
    {
        //arrange
        const int expected = 42;

        //act
        var actual = PlainValueChunk.FromObject(expected).Build();

        //assert
        actual.Should().Be("42");
    }

    [Fact]
    public void FromObject_WhenCalledWithDouble_ThenConvertsToStringWithCulture()
    {
        //arrange
        const double expected = 3.14;

        //act
        var actual = PlainValueChunk.FromObject(expected, CultureInfo.InvariantCulture).Build();

        //assert
        actual.Should().Be("3.14");
    }

    [Fact]
    public void FromObject_WhenCalledWithDoubleWithPolishCulture_ThenConvertsToStringWithComma()
    {
        //arrange
        const double expected = 3.14;

        //act
        var actual = PlainValueChunk.FromObject(expected, new CultureInfo("pl-PL")).Build();

        //assert
        actual.Should().Be("3,14");
    }

    [Fact]
    public void FromObject_WhenCalledWithCustomObject_ThenCallsToString()
    {
        //arrange
        var customObject = new CustomObject("custom value");

        //act
        var actual = PlainValueChunk.FromObject(customObject).Build();

        //assert
        actual.Should().Be("custom value");
    }

    [Fact]
    public void FromObject_WhenCalledWithNull_ThenThrowsNullReferenceException()
    {
        //arrange
        //act
        var act = () => PlainValueChunk.FromObject(null!);

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_WhenCalledWithEmptyString_ThenReturnsEmptyString()
    {
        //arrange
        const string expected = "";

        //act
        var actual = new PlainValueChunk(expected).Build();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Build_WhenCalledWithEmptyStringBuilder_ThenReturnsEmptyString()
    {
        //arrange
        var stringBuilder = new StringBuilder();

        //act
        var actual = new PlainValueChunk(stringBuilder).Build();

        //assert
        actual.Should().Be("");
    }

    [Fact]
    public void Build_WhenCalledWithSpecialCharacters_ThenReturnsSpecialCharacters()
    {
        //arrange
        const string expected = "test\nvalue\twith\r\nspecial";

        //act
        var actual = new PlainValueChunk(expected).Build();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Build_WhenCalledWithUnicodeCharacters_ThenReturnsUnicodeCharacters()
    {
        //arrange
        const string expected = "test 测试 test";

        //act
        var actual = new PlainValueChunk(expected).Build();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Build_WhenCalledWithLongString_ThenReturnsLongString()
    {
        //arrange
        var expected = new string('a', 1000);

        //act
        var actual = new PlainValueChunk(expected).Build();

        //assert
        actual.Should().Be(expected);
    }

    private class CustomObject(string value)
    {
        public override string ToString() => value;
    }
}
