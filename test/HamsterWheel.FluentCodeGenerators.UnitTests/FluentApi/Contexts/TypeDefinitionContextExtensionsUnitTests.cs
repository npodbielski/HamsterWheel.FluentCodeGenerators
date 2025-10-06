using System.Net;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class TypeDefinitionContextExtensionsUnitTests
{
    private readonly TypeNameChunk _chunk;
    private readonly TypeUsageContext _sut;

    public TypeDefinitionContextExtensionsUnitTests()
    {
        _chunk = TypeNameChunk.From<int>();
        var context = new SourceCodeFileContext();
        _sut = new TypeUsageContext(context, _chunk);
    }

    [Fact]
    public void From_WhenCalled_ThenRendersCorrectType()
    {
        //arrange
        var expected = "IPAddress";

        //act
        _sut.From<IPAddress>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalled_ThenAddsUsings()
    {
        //arrange
        var expected = "System.Net";

        //act
        _sut.From<IPAddress>();

        //assert
        _sut.Usings.Should().RenderAs($"using {expected};");
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenRendersCorrectType()
    {
        //arrange
        var expected = "IQueryable<int>";

        //act
        _sut.From(typeof(IQueryable<>)).WithGenericArgument<int>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenAddsUsings()
    {
        //arrange
        //act
        _sut.From(typeof(IQueryable<>)).WithGenericArgument<IClassContext>();

        //assert
        _sut.Usings.Should().RenderAs("""
                                      using System.Linq;
                                      using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
                                      """);
    }
}