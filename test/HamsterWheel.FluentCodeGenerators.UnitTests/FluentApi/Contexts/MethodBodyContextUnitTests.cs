using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class MethodBodyContextUnitTests
{
    private readonly MethodBodyContext _sut;
    private readonly AppendableChunk _chunk;

    public MethodBodyContextUnitTests()
    {
        _chunk = new AppendableChunk();
        _sut = new MethodBodyContext(new SourceCodeFileContext(), _chunk, []);
    }

    [Fact]
    public void Append_WhenCalledWithString_ThenRendersCorrectly()
    {
        //arrange
        var expected = "//comment";

        //act
        _sut.Append("//comment");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Append_WhenCalledWithCodeChunk_ThenRendersCorrectly()
    {
        //arrange
        var expected = "static";

        //act
        _sut.Append(new StaticKeywordChunk());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void InIndent_WhenCalled_ThenPutsNestedCodeInIndent()
    {
        //arrange
        var expected = """

                           //indented comment

                       """;

        //act
        _sut.InIndent(i => i.AppendLine("//indented comment"));

        //assert
        _chunk.Should().RenderAs(expected, noTrim: true);
    }
}