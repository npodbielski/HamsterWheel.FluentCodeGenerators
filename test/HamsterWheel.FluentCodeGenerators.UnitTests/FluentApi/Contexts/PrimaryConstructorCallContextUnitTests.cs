using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class PrimaryConstructorCallContextUnitTests
{
    private readonly PrimaryConstructorCallChunk _chunk;
    private readonly PrimaryConstructorCallContext _sut;

    public PrimaryConstructorCallContextUnitTests()
    {
        _chunk = new PrimaryConstructorCallChunk();
        var context = new SourceCodeFileContext();
        _sut = PrimaryConstructorCallContext.From(context, _chunk);
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenAdsThisParameterNameToParameterNamesArray()
    {
        //arrange
        //act
        _sut.WithParameter(p => p.MakeNamed("firstParam"));

        //assert
        _sut.ParametersNames.Select(c => c.NameAsString).Should().BeEquivalentTo("firstParam");
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenAddsParameter()
    {
        //arrange
        var expected = """
                       (firstParam: "test")
                       """;
        
        //act
        _sut.WithParameter(p => p.MakeNamed("firstParam").UseStringValue("test"));

        //assert
        _chunk.Should().RenderAs(expected);
    }
}