using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class PropertyContextExtensionsUnitTests
{
    private PropertyContext _sut;
    private readonly PropertyDefinitionChunk _chunk;

    public PropertyContextExtensionsUnitTests()
    {
        _chunk = PropertyDefinitionChunk.From("Prop0");
        _sut = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void OfTypeOfT1_WhenCalled_ThenSetsType()
    {
        //arrange
        var expected = "public DateOnly Prop0 { get; set; }";

        //act
        _sut.OfType<DateOnly>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithNameOfInitializer_WhenCalled_ThenSetsInitializer()
    {
        //arrange
        var expected = "public string Prop0 { get; set; } = nameof(DateOnly);";

        //act
        _sut.WithNameOfInitializer(typeof(DateOnly).ToNameInNamespace());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithNewInitializer_WhenCalled_ThenSetsInitializer()
    {
        //arrange
        var expected = "public string Prop0 { get; set; } = new();";

        //act
        _sut.WithNewInitializer();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithNewInitializer_WhenCalledWithChunk_ThenSetsInitializer()
    {
        //arrange
        var expected = "public string Prop0 { get; set; } = \"test\";";

        //act
        _sut.WithInitializer<CustomInitializeChunk>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithExpressionBody_WhenCalledWithString_ThenSetsExpressionBody()
    {
        //arrange
        var expected = "public string Prop0 => null;";

        //act
        _sut.WithExpressionBody("null");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    public class CustomInitializeChunk() : PlainValueChunk("test".Quote());
}