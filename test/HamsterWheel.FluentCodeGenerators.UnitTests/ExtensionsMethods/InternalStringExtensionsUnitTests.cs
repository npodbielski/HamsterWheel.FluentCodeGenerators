using FluentAssertions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class InternalStringExtensionsUnitTests
{
    [Theory]
    [InlineData("values", "value")]
    [InlineData("blocks", "block")]
    [InlineData("flows", "flow")]
    [InlineData("scopes", "scope")]
    [InlineData("entities", "entity")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToSingular_WhenCalledWithString_ThenReturnsCorrectSingularVersion(string? plural, string? singular)
    {
        //arrange
        //act
        var actual = plural.ToSingular();

        //assert
        actual.Should().Be(singular);
    }

    [Theory]
    [InlineData("value", "values")]
    [InlineData("block", "blocks")]
    [InlineData("flow", "flows")]
    [InlineData("scope", "scopes")]
    [InlineData("entity", "entities")]
    [InlineData("dex", "dexes")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToPlural_WhenCalledWithString_ThenReturnsCorrectSingularVersion(string? singular, string? plural)
    {
        //arrange
        //act
        var actual = singular.ToPlural();

        //assert
        actual.Should().Be(plural);
    }

    [Theory]
    [InlineData("value", "Value")]
    [InlineData("valuevalue", "Valuevalue")]
    [InlineData("int", "Int")]
    [InlineData("myEnum", "MyEnum")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToPascalCase_WhenCalled_ThenReturnsCorrectString(string? input, string expected)
    {
        //arrange
        //act
        var actual = input.ToPascalCase();

        //assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("Value", "value")]
    [InlineData("ValueValue", "valueValue")]
    [InlineData("Int", "int")]
    [InlineData("MyEnum", "myEnum")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToCamelCase_WhenCalled_ThenReturnsCorrectString(string? input, string expected)
    {
        //arrange
        //act
        var actual = input.ToCamelCase();

        //assert
        actual.Should().Be(expected);
    }
}