using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ExpressionBodyContextUnitTests
{
    private readonly ExpressionBodyContext _sut;
    private readonly AppendableChunk _chunk;

    public ExpressionBodyContextUnitTests()
    {
        _chunk = new AppendableChunk();
        _sut = new ExpressionBodyContext(new SourceCodeFileContext(), _chunk, []);
    }

    [Fact]
    public void Append_WhenCalledWithString_ThenRendersCorrectly()
    {
        //arrange
        var expected = "//test";

        //act
        _sut.Append("//test");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Append_WhenCalledWithCodeChunk_ThenRendersCorrectly()
    {
        //arrange
        var expected = "test";

        //act
        _sut.Append(new PlainValueChunk("test"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void InIndent_WhenCalled_ThenRendersInIndent()
    {
        //arrange
        var expected = """
                       new()
                           {
                               //test
                           }
                       """;

        //act
        _sut.AppendLine("");
        _sut.InIndent(i=>i.Append("""
                                  new()
                                  {
                                      //test
                                  }
                                  """));

        //assert
        _chunk.Should().RenderAs(expected);
    }
}