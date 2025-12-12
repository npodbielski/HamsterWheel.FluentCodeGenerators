using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ExpressionBodyContextExtensionsUnitTests
{
    private readonly ExpressionBodyContext _sut;
    private readonly AppendableChunk _chunk;

    public ExpressionBodyContextExtensionsUnitTests()
    {
        _chunk = new AppendableChunk();
        _sut = new ExpressionBodyContext(new SourceCodeFileContext(), _chunk, []);
    }
    
    [Fact]
    public void ConfigureUsing_When_Then()
    {
        //arrange
        var expected = "new ()";

        //act
        _sut.ConfigureUsing<ExpressionBodyContextConfigurator>();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}

public class ExpressionBodyContextConfigurator : IContextConfigurator<IExpressionBodyContext>
{
    public void Configure(IExpressionBodyContext context) => context.Append("new ()");
}