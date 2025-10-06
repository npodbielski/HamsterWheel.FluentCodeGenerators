using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Tokens;

public class PascalCaseNameUnitTests
{
    [Fact]
    public void RemoveSuffix_WhenCalled_ThenRemovesSuffix()
    {
        //arrange
        var expected = "EntityMapper";
        var name = new PascalCaseName("EntityMapperOverride");

        //act
        var actual = name.RemoveSuffix("Override");

        //assert
        actual.NameAsString.Should().Be(expected);
    }
}