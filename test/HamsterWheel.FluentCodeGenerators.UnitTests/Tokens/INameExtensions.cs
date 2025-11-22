using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Tokens;

public class INameExtensionsUnitTests
{
    [Fact]
    public void ToPlural_WhenCalled_ThenReturnsPlural()
    {
        //arrange
        var expected = "modules";
        var name = new CamelCaseName("module");

        //act
        var actual = name.ToPlural();

        //assert
        actual.NameAsString.Should().Be(expected);
    }

    [Fact]
    public void ToSingular_WhenCalled_ThenReturnsPlural()
    {
        //arrange
        var expected = "module";
        var name = new CamelCaseName("modules");

        //act
        var actual = name.ToSingular();

        //assert
        actual.NameAsString.Should().Be(expected);
    }

    [Theory]
    [InlineData("module", "m")]
    [InlineData("Scopes", "s")]
    public void FirstLetterLower_WhenCalled_ThenReturnsCorrectString(string nameString, string firstLetter)
    {
        //arrange
        var name = new CamelCaseName(nameString);

        //act
        var actual = name.FirstLetterLower();

        //assert
        actual.Should().Be(firstLetter);
    }
}