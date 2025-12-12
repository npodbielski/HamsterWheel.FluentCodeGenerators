using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class TypeOfExpressionChunkUnitTests
{
    [Fact]
    public void WhenRendered_ThenRendersCorrectly()
    {
        //arrange
        var expected = "typeof(int)";

        //act
        var actual = new TypeOfExpressionChunk("int");

        //assert
        actual.Should().RenderAs(expected);
    }
}