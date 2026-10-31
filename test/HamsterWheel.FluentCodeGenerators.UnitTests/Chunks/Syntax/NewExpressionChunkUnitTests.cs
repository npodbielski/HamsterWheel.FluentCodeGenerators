using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class NewExpressionChunkUnitTests
{
    [Fact]
    public void WhenRenderedWithoutIdentifier_ThenRendersEmptyNew()
    {
        //arrange
        var expected = "new()";

        //act
        var actual = new NewExpressionChunk();

        //assert
        actual.Should().RenderAs(expected);
    }

    [Fact]
    public void WhenRenderedWithStringIdentifier_ThenRendersNewWithIdentifier()
    {
        //arrange
        var expected = "new SomeClass()";

        //act
        var actual = new NewExpressionChunk("SomeClass");

        //assert
        actual.Should().RenderAs(expected);
    }

    [Fact]
    public void WhenRenderedWithNameInNamespace_ThenRendersNewWithIdentifier()
    {
        //arrange
        var expected = "new SomeClass()";
        var identifier = NameInNamespace.From("SomeClass", "System");

        //act
        var actual = new NewExpressionChunk(identifier);

        //assert
        actual.Should().RenderAs(expected);
    }

    [Fact]
    public void WhenCreatedWithNameInNamespace_ThenReturnsCorrectNamespace()
    {
        //arrange
        var identifier = NameInNamespace.From("List", "System.Collections.Generic");

        //act
        var actual = new NewExpressionChunk(identifier);

        //assert
        Assert.Single(actual.Namespace);
    }

    [Fact]
    public void WhenCreatedWithString_ThenReturnsEmptyNamespaceArray()
    {
        //arrange
        //act
        var actual = new NewExpressionChunk("SomeClass");

        //assert
        Assert.Empty(actual.Namespace);
    }
}
