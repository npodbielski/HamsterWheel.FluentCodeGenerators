using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Tokens;

public class ScreamingSnakeCaseNameUnitTests
{
    [Fact]
    public void ToScreamingSnakeCaseName_WhenCalledWithCamelCaseString_ThenReturnsWrappedValue()
    {
        //arrange
        var input = "screamingCaseName";

        //act
        var result = input.ToScreamingSnakeCaseName();

        //assert
        result.NameAsString.Should().Be("screamingCaseName");
    }

    [Fact]
    public void ToScreamingSnakeCaseName_WhenCalledWithPascalCaseString_ThenReturnsWrappedValue()
    {
        //arrange
        var input = "PascalCaseName";

        //act
        var result = input.ToScreamingSnakeCaseName();

        //assert
        result.NameAsString.Should().Be("PascalCaseName");
    }

    [Fact]
    public void ToScreamingSnakeCaseName_WhenCalledWithNullString_ThenReturnsEmptyValue()
    {
        //arrange
        string? input = null;

        //act
        var result = input.ToScreamingSnakeCaseName();

        //assert
        result.NameAsString.Should().Be("");
    }

    [Fact]
    public void ImplicitOperator_WhenConvertingFromString_ThenAppliesScreamingSnakeCase()
    {
        //arrange
        ScreamingSnakeCaseName name = "testValue";

        //act
        var result = name.NameAsString;

        //assert
        result.Should().Be("TEST_VALUE");
    }

    [Fact]
    public void ImplicitOperator_WhenConvertingToString_ThenReturnsCorrectValue()
    {
        //arrange
        ScreamingSnakeCaseName name = "anotherTest";
        string? str = name;

        //act & assert
        str.Should().Be("ANOTHER_TEST");
    }

    [Fact]
    public void ToString_WhenCalled_ThenReturnsCorrectValue()
    {
        //arrange
        var name = new ScreamingSnakeCaseName("SOME_VALUE");

        //act
        var result = name.ToString();

        //assert
        result.Should().Be("SOME_VALUE");
    }
}
