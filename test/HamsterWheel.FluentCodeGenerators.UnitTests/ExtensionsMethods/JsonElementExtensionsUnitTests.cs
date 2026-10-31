using System.Text.Json;
using FluentAssertions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class JsonElementExtensionsUnitTests
{
    [Theory]
    [InlineData("12", "12")]
    [InlineData("1.5", "1.5")]
    [InlineData("true", "true")]
    [InlineData("false", "false")]
    [InlineData("null", "null")]
    [InlineData("\"text\"", "\"text\"")]
    [InlineData("\"say \\\"hi\\\"\"", "\"say \\\"hi\\\"\"")]
    [InlineData("[1,2]", "[1,2]")]
    [InlineData("{\"a\":1}", "{\"a\":1}")]
    public void ToCodeLiteral_WhenCalled_ThenReturnsTheCSharpLiteralOfTheJson(string json, string expected)
    {
        //arrange
        using var document = JsonDocument.Parse(json);

        //act
        var actual = document.RootElement.ToCodeLiteral();

        //assert
        actual.Should().Be(expected);
    }
}
