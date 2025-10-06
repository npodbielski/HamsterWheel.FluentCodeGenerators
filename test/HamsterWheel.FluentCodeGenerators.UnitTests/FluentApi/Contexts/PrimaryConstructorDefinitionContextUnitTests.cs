using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class PrimaryConstructorDefinitionContextUnitTests
{
    private readonly PrimaryConstructorDefinitionChunk _chunk;
    private readonly PrimaryConstructorDefinitionContext _sut;

    public PrimaryConstructorDefinitionContextUnitTests()
    {
        _chunk = new PrimaryConstructorDefinitionChunk([]);
        var context = new SourceCodeFileContext();
        _sut = (PrimaryConstructorDefinitionContext)PrimaryConstructorDefinitionContext.From(context, _chunk);
    }

    [Fact]
    public void Parameters_WhenCalled_ThenAddsParameterToParameterList()
    {
        //arrange
        //act
        _sut.WithParameter(p => p.Named("otherParam"));

        //assert
        _sut.Parameters.Should().Contain(p => p.Key == "otherParam");
    }

    [Fact]
    public void Parameters_WhenCalledSecondTimeWithTheSameName_ThenThrows()
    {
        //arrange
        _sut.WithParameter(p => p.Named("otherParam"));
        var action = () => _sut.WithParameter(p => p.Named("otherParam"));

        //act
        var actual = action.Should().Throw<DuplicatedParameterException>();

        //assert
        actual.WithMessage("*otherParam*");
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenAddsParameter()
    {
        //arrange
        var expected = "(string stringParam)";

        //act
        _ = _sut.WithParameter(p => { });

        //assert
        _chunk.Should().RenderAs(expected);
    }
}