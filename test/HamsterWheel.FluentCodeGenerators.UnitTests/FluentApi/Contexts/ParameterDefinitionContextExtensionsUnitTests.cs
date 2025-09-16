using System.Runtime.InteropServices;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ParameterDefinitionContextExtensionsUnitTests
{
    private readonly ParameterDefinitionContext _sut;
    private readonly ParameterDefinitionChunk _chunk;

    public ParameterDefinitionContextExtensionsUnitTests()
    {
        _chunk = new ParameterDefinitionChunk();
        _sut = new ParameterDefinitionContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void OfType_WhenCalled_ThenParameterHaveCorrectType()
    {
        //arrange
        var expected = """
                       int intParam
                       """;

        //act
        _sut.OfType<int>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenCalled_ThenParameterHaveCorrectType()
    {
        //arrange
        var expected = """
                       [Optional]string stringParam
                       """;

        //act
        _sut.WithAttribute<OptionalAttribute>();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}