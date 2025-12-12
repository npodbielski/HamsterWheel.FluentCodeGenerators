using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class PrimaryConstructorDefinitionContextExtensionsUnitTests
{
    private readonly PrimaryConstructorDefinitionChunk _chunk;
    private readonly PrimaryConstructorDefinitionContext _sut;

    public PrimaryConstructorDefinitionContextExtensionsUnitTests()
    {
        _chunk = new PrimaryConstructorDefinitionChunk([]);
        var context = new SourceCodeFileContext();
        _sut = (PrimaryConstructorDefinitionContext)PrimaryConstructorDefinitionContext.From(context, _chunk);
    }

    [Fact]
    public void WithParameterOfT1_WhenCalledWithJustName_ThenParameterHaveCorrectName()
    {
        //arrange
        var expected = "(decimal myParam)";

        //act
        _ = _sut.WithParameter<decimal>("myParam");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameterOfT1_WhenCalled_ThenParameterHaveCorrectType()
    {
        //arrange
        var expected = "(DateTime dateTime)";

        //act
        _ = _sut.WithParameter<DateTime>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameterOfT1_WhenCalled_ThenAddsUsing()
    {
        //arrange
        var expected = "using System;";

        //act
        _ = _sut.WithParameter<DateTime>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void ConfigureUsing_WhenCalled_ThenRunsConfigurer()
    {
        //arrange
        var expected = "(HttpClient myParam)";

        //act
        _ = _sut.ConfigureUsing<DummyConfigurer>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    private class DummyConfigurer : IContextConfigurator<IPrimaryConstructorDefinitionContext>
    {
        public void Configure(IPrimaryConstructorDefinitionContext context)
        {
            context.WithParameter<HttpClient>("myParam");
        }
    }
}