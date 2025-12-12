using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IParameterValueContextExtensionsUnitTests
{
    private readonly ParameterValueContext _sut;
    private readonly ParameterValueChunk _chunk;

    public IParameterValueContextExtensionsUnitTests()
    {
        _chunk = ParameterValueChunk.From("2", FluentApiSettings.DefaultCulture);
        _sut = new ParameterValueContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void UseExpression_WhenCalledWithIName_ThenParameter()
    {
        //arrange
        var expected = "MyClass";

        //act
        _sut.UseExpression((IName)new CamelCaseName("MyClass"));

        //assert
        _chunk.Should().RenderAs(expected);
    }
}