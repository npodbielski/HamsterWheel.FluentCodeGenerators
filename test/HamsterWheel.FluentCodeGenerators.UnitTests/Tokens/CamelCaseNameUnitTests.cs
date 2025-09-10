using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Tokens;

public class CamelCaseNameUnitTests
{
    [Fact]
    public void RemoveSuffix_WhenCalled_ThenRemovesSuffix()
    {
        //arrange
        var expected = "entityMapper";
        var name = new CamelCaseName("entityMapperOverride");

        //act
        var actual = name.RemoveSuffix("Override");

        //assert
        actual.NameAsString.Should().Be(expected);
    }
}