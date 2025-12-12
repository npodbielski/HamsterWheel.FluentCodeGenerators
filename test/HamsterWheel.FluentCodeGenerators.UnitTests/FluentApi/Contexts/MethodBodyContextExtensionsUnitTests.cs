using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class MethodBodyContextExtensionsUnitTests
{
    private readonly MethodBodyContext _sut;
    private readonly AppendableChunk _chunk;

    public MethodBodyContextExtensionsUnitTests()
    {
        _chunk = new AppendableChunk();
        _sut = new MethodBodyContext(new SourceCodeFileContext(), _chunk, []);
    }

    [Fact]
    public void AppendOfT1_WhenCalledWithChunk_ThenRendersCorrectly()
    {
        //arrange
        var expected = "static";

        //act
        _sut.Append<StaticKeywordChunk>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void AppendLineOfT1_WhenCalledWithChunk_ThenRendersCorrectly()
    {
        //arrange
        var expected = """

                       static 

                       """;

        //act
        _sut.AppendLine<StaticKeywordChunk>();

        //assert
        _chunk.Should().RenderAs(expected, noTrim: true);
    }

    [Fact]
    public void AppendLine_WhenCalledWithCodeChunk_ThenRendersCorrectly()
    {
        //arrange
        var expected = """

                       static 

                       """;

        //act
        _sut.AppendLine(new StaticKeywordChunk());

        //assert
        _chunk.Should().RenderAs(expected, noTrim: true);
    }

    [Fact]
    public void ConfigureUsing_WhenCalledWithCodeChunk_ThenRendersCorrectly()
    {
        //arrange
        var expected = """
                       //test
                       """;

        //act
        _sut.ConfigureUsing<TestMethodBodyConfigurator>();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}

public class TestMethodBodyConfigurator : IContextConfigurator<IMethodBodyContext>
{
    public void Configure(IMethodBodyContext context) => context.Append("//test");
}