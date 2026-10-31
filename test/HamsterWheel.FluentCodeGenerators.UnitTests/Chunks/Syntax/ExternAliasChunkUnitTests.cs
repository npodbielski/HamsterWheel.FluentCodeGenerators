using System.Text;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class ExternAliasChunkUnitTests
{
    [Fact]
    public void Ctor_WhenCalled_ThenStoresAlias()
    {
        // arrange
        const string alias = "myAlias";

        // act
        var chunk = new ExternAliasChunk(alias);

        // assert
        chunk.Should().NotBeNull();
    }

    [Fact]
    public void AppendChunks_WhenCalled_ThenAppendsExternAliasStatement()
    {
        // arrange
        const string alias = "myAlias";
        const string expected = "extern alias myAlias;\n";
        var stringBuilder = new StringBuilder();
        var chunk = new ExternAliasChunk(alias);

        // act
        var result = chunk.AppendChunks(stringBuilder);

        // assert
        result.Should().BeTrue();
        stringBuilder.ToString().Should().Be(expected);
    }

    [Fact]
    public void AppendChunks_WhenCalledWithDifferentAlias_ThenAppendsCorrectStatement()
    {
        // arrange
        const string alias = "root";
        const string expected = "extern alias root;\n";
        var stringBuilder = new StringBuilder();
        var chunk = new ExternAliasChunk(alias);

        // act
        var result = chunk.AppendChunks(stringBuilder);

        // assert
        result.Should().BeTrue();
        stringBuilder.ToString().Should().Be(expected);
    }

    [Fact]
    public void AppendChunks_WhenCalledWithEmptyAlias_ThenAppendsCorrectStatement()
    {
        // arrange
        const string alias = "";
        const string expected = "extern alias ;\n";
        var stringBuilder = new StringBuilder();
        var chunk = new ExternAliasChunk(alias);

        // act
        var result = chunk.AppendChunks(stringBuilder);

        // assert
        result.Should().BeTrue();
        stringBuilder.ToString().Should().Be(expected);
    }
}
