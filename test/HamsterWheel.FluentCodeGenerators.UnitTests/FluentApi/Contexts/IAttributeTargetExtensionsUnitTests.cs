using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Generators;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IAttributeTargetExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _chunk;
    private const string Version = "0.4.0.0";

    public IAttributeTargetExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        var context = new SourceCodeFileContext();
        _sut = new(context, _chunk);
        FluentApiSettings.GeneratorAssembly = typeof(SourceCodeFileGeneratorBase).Assembly;
    }

    [Fact]
    public void WithGeneratedCodeAttr_WhenRendered_ThenHaveGeneratedCodeAttribute()
    {
        //arrange
        const string expected = $$"""
                                  [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                                  public class MyClass
                                  {

                                  }
                                  """;

        //act
        _sut.WithGeneratedCodeAttr();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGeneratedCodeAttr_WhenAssemblyChanged_ThenHaveGeneratedCodeAttributeHaveThisAssembly()
    {
        //arrange
        const string expected = """
                                [GeneratedCode("HamsterWheel.FluentCodeGenerators.UnitTests", "Version=1.0.0.0")]
                                public class MyClass
                                {

                                }
                                """;
        FluentApiSettings.GeneratorAssembly = GetType().Assembly;

        //act
        _sut.WithGeneratedCodeAttr();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenCalledWithTypeOfAttribute_ThenRendersCorrectAttribute()
    {
        //arrange
        const string expected = """
                                [ExcludeFromCodeCoverage]
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithAttribute(typeof(ExcludeFromCodeCoverageAttribute));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenCalledWithTypeOfAttribute_ThenAddsUsing()
    {
        //arrange
        const string expected = """
                                using System.Diagnostics.CodeAnalysis;
                                """;

        //act
        _sut.WithAttribute(typeof(ExcludeFromCodeCoverageAttribute));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenCalledWithNameInNamespace_ThenRendersCorrectAttribute()
    {
        //arrange
        const string expected = """
                                [ExcludeFromCodeCoverage]
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithAttribute(NameInNamespace.From("ExcludeFromCodeCoverageAttribute", "System.Diagnostics.CodeAnalysis"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenCalledWithNameInNamespace_ThenAddsUsing()
    {
        //arrange
        const string expected = "using System.Diagnostics.CodeAnalysis;";

        //act
        _sut.WithAttribute(NameInNamespace.From("ExcludeFromCodeCoverageAttribute", "System.Diagnostics.CodeAnalysis"));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenNotAttributeType_ThenThrows()
    {
        //arrange
        var action = () => _sut.WithAttribute(typeof(int));

        //act
        var exception = action.Should().Throw<ArgumentException>();

        //assert
        exception.WithMessage("*must inherit from Attribute class!");
    }

    [Theory]
    [InlineData(nameof(ExcludeFromCodeCoverageAttribute))]
    [InlineData("ExcludeFromCodeCoverage")]
    public void WithAttribute_WhenCalledWithString_ThenRendersCorrectAttribute(string attribute)
    {
        //arrange
        const string expected = """
                                [ExcludeFromCodeCoverage]
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithAttribute(attribute);

        //assert
        _chunk.Should().RenderAs(expected);
    }
}