using System.Data.Common;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class FieldContextExtensionsUnitTests
{
    private readonly FieldContext _sut;
    private readonly FieldDefinitionChunk _chunk;

    public FieldContextExtensionsUnitTests()
    {
        _chunk = FieldDefinitionChunk.From("_test");
        _sut = FieldContext.From(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void OfTypeT1_WhenCalledWithTypeArgument_ThenSetsTypeOfTheField()
    {
        //arrange
        var expected = "private DateTime _test;";

        //act
        _sut.OfType<DateTime>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfTypeT1_WhenCalledWithTypeArgument_ThenAddsUsings()
    {
        //arrange
        var expected = """
                       using System.Data.Common;
                       using System.Linq;
                       """;

        //act
        _sut.OfType<IQueryable<DbConnection>>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }
}