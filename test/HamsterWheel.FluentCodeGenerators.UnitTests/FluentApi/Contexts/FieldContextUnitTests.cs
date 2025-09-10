using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class FieldContextUnitTests
{
    private readonly FieldContext _sut;
    private readonly FieldDefinitionChunk _chunk;

    public FieldContextUnitTests()
    {
        _chunk = FieldDefinitionChunk.From("_test");
        _sut = FieldContext.From(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void Named_WhenCalled_ThenSetsNameOfTheField()
    {
        //arrange
        var expected = "private object _newName;";

        //act
        _sut.Named("newName");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeStatic_WhenCalled_ThenFieldIsStatic()
    {
        //arrange
        var expected = """
                       private static object _test;
                       """;

        //act
        _sut.MakeStatic();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeNullable_WhenCalledWithTypeArgument_ThenAddsUsings()
    {
        //arrange
        var expected = """
                       private object? _test;
                       """;

        //act
        _sut.MakeNullable();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void DisableNullabilityWarning_WhenCalledWithTypeArgument_ThenAddsUsings()
    {
        //arrange
        var expected = """
                       private object _test = default!;
                       """;

        //act
        _sut.DisableNullabilityWarning();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInitializer_WhenCalledWithTypeArgument_ThenAddsUsings()
    {
        //arrange
        var expected = """
                       private object _test = new();
                       """;

        //act
        _sut.WithInitializer("new()");

        //assert
        _chunk.Should().RenderAs(expected);
    }
}