using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IMethodBodyContextExtensionsUnitTests
{
    private readonly MethodBodyContext _sut;
    private readonly AppendableChunk _chunk;

    public IMethodBodyContextExtensionsUnitTests()
    {
        _chunk = new AppendableChunk();
        _sut = new MethodBodyContext(new SourceCodeFileContext(), _chunk, []);
    }

    [Fact]
    public void AppendReturn_WhenCalled_ThenRendersCorrectly()
    {
        //arrange
        var expected = """
                       return new();
                       """;

        //act
        _sut.AppendReturn("new()");

        //assert
        _chunk.Should().RenderAs(expected);
    }
}