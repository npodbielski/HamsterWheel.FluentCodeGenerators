using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IParameterBagContextExtensionsUnitTests
{
    private readonly PrimaryConstructorCallContext _sut;
    private readonly PrimaryConstructorCallChunk _callChunk;

    public IParameterBagContextExtensionsUnitTests()
    {
        _callChunk = new PrimaryConstructorCallChunk();
        _sut = new PrimaryConstructorCallContext(new SourceCodeFileContext(), _callChunk);
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenAddsParameter()
    {
        //arrange
        var expected = """
                       ("test")
                       """;

        //act
        _sut.WithParameter("test");

        //assert
        _callChunk.Should().RenderAs(expected);
    }
}