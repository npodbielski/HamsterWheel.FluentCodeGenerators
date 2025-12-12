using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class SingleAttributeContextExtensionsUnitTests
{
    private readonly SingleAttributeContext _sut;
    private readonly AttributeDefinitionChunk _chunk;

    public SingleAttributeContextExtensionsUnitTests()
    {
        _chunk = AttributeDefinitionChunk.From("NotNullAttribute");
        _sut = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void FromOfT1_WhenCalled_ThenSetsType()
    {
        //arrange
        var expected = "[ExcludeFromCodeCoverage]";

        //act
        _sut.From<ExcludeFromCodeCoverageAttribute>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData(nameof(ExcludeFromCodeCoverageAttribute))]
    [InlineData("ExcludeFromCodeCoverage")]
    public void From_WhenCalledWithString_ThenSetsType(string type)
    {
        //arrange
        var expected = "[ExcludeFromCodeCoverage]";

        //act
        _sut.From(type);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalledWithString_ThenSetsParameter()
    {
        //arrange
        var expected = "[NotNull(1)]";

        //act
        _sut.WithParameter("1");

        //assert
        _chunk.Should().RenderAs(expected);
    }
}