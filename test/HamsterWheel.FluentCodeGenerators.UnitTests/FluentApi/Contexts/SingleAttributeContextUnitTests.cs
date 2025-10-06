using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class SingleAttributeContextUnitTests
{
    private SingleAttributeContext _sut;
    private readonly AttributeDefinitionChunk _chunk;

    public SingleAttributeContextUnitTests()
    {
        _chunk = AttributeDefinitionChunk.From("NotNullAttribute");
        _sut = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void From_WhenCalled_ThenSetsType()
    {
        //arrange
        var expected = "[ExcludeFromCodeCoverage]";

        //act
        _sut.From(t => t.From<ExcludeFromCodeCoverageAttribute>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalled_ThenAddsUsing()
    {
        //arrange
        var expected = "using System.Diagnostics.CodeAnalysis;";

        //act
        _sut.From(t => t.From<ExcludeFromCodeCoverageAttribute>());

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalledWithString_ThenAddsUsing()
    {
        //arrange
        var expected = "[ExcludeFromCodeCoverage]";

        //act
        _sut.From(nameof(ExcludeFromCodeCoverageAttribute));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenSetsParameter()
    {
        //arrange
        var expected = "[NotNull(\"dataMember\")]";

        //act
        _sut.WithParameter(p => p.UseExpression("dataMember".Quote()));

        //assert
        _chunk.Should().RenderAs(expected);
    }
}