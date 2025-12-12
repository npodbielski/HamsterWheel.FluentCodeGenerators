using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ConstructorContextUnitTests
{
    private ConstructorContext _sut;
    private CtorDefinitionChunk _chunk;
    private const string FirstParamName = "param0";
    private const string TypeName = "ClassWithCtor";

    public ConstructorContextUnitTests()
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
    public void WhenHaveThisCtorThis_ThenRendersCorrectly()
    {
        //arrange
        _chunk = new CtorDefinitionChunk(
            MemberVisibility.Public,
            new ParametersDefinitionListChunk([
                new ParameterDefinitionChunk(TypeNameChunk.String, FirstParamName)
            ]),
            new ThisCtorChunk());
        _chunk.SetName(TypeDefinitionWithPrimaryConstructorChunk.FromName(TypeName));
        _sut = new(new SourceCodeFileContext(), _chunk);
        var expected = $$"""
                         public {{TypeName}}(string {{FirstParamName}}) : this()
                         {
                         }
                         """;

        //act
        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WhenHaveBaseCtorBase_ThenRendersCorrectly()
    {
        //arrange
        _chunk = new CtorDefinitionChunk(
            MemberVisibility.Public,
            new ParametersDefinitionListChunk([
                new ParameterDefinitionChunk(TypeNameChunk.String, FirstParamName)
            ]),
            new BaseCtorChunk());
        _chunk.SetName(TypeDefinitionWithPrimaryConstructorChunk.FromName(TypeName));
        _sut = new(new SourceCodeFileContext(), _chunk);
        var expected = $$"""
                         public {{TypeName}}(string {{FirstParamName}}) : base()
                         {
                         }
                         """;

        //act
        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBody_WhenCalled_ThenRendersCorrectly()
    {
        //arrange
        var expected = $$"""
                         public {{TypeName}}(string {{FirstParamName}})
                         {
                             {{FirstParamName}}
                         }
                         """;

        //act
        _sut.WithBody(b => b.Append(b.ParametersNames[0]));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithExpressionBody_WhenCalled_ThenRendersCorrectly()
    {
        //arrange
        var expected = $"public {TypeName}(string {FirstParamName}) => {FirstParamName};";

        //act
        _sut.WithExpressionBody(b => b.Append(b.ParameterNames[0]));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenRendersCorrectly()
    {
        //arrange
        var expected = $$"""
                         public {{TypeName}}(string {{FirstParamName}}, string newParam)
                         {
                         }
                         """;

        //act
        _sut.WithParameter(p => p.Named("newParam"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenAvailableInParametersCollection()
    {
        //arrange
        //act
        _sut.WithParameter(p => p.Named("newParam"));

        //assert
        _sut.ParametersNames.Should().HaveCount(2);
    }

    [Fact]
    public void WithParameter_WhenCalled_ThenAvailableInBody()
    {
        //arrange
        _sut.WithParameter(p => p.Named("newParam"));
        IMethodBodyContext? bodyContext = null;

        //act
        _sut.WithBody(b => bodyContext = b);

        //assert
        bodyContext.Should().NotBeNull();
        bodyContext.ParametersNames.Should().HaveCount(2);
    }
}