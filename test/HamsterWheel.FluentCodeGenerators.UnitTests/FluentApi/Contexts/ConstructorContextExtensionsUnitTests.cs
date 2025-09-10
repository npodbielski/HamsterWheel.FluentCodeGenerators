using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ConstructorContextExtensionsUnitTests
{
    private readonly ConstructorContext _sut;
    private readonly CtorDefinitionChunk _chunk;
    private const string FirstParamName = "param0";
    private const string TypeName = "ClassWithCtor";

    public ConstructorContextExtensionsUnitTests()
    {
        _chunk = new CtorDefinitionChunk(
            MemberVisibility.Public,
            new ParametersDefinitionListChunk([
                new ParameterDefinitionChunk(TypeNameChunk.String, FirstParamName)
            ]));
        _chunk.SetName(TypeDefinitionWithPrimaryConstructorChunk.FromName(TypeName));
        var context = new SourceCodeFileContext();
        _sut = new(context, _chunk);
    }

    [Fact]
    public void ConfigureUsing_When_Then()
    {
        //arrange
        const string expected = $$"""
                                  public {{TypeName}}(string {{FirstParamName}}, string param1)
                                  {
                                  }
                                  """;

        //act
        _sut.ConfigureUsing<ConstructorDefinitionConfigurator>();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}

public class ConstructorDefinitionConfigurator : IContextConfigurator<ConstructorContext>
{
    public void Configure(ConstructorContext context)
    {
        context.WithParameter(p => p.Named("param1"));
    }
}