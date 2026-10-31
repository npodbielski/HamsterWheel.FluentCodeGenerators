using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IAttributeTargetExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _chunk;
    private string Version => this.GetThisObjectTypeAssemblyVersion();

    public IAttributeTargetExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        var context = new SourceCodeFileContext();
        _sut = new(context, _chunk);
    }

    [Fact]
    public void WithGeneratedCodeAttr_WhenRendered_ThenHaveGeneratedCodeAttribute()
    {
        //arrange
        var expected = $$"""
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
        string expected = $$"""
                            [GeneratedCode("HamsterWheel.FluentCodeGenerators.UnitTests", "Version={{Version}}")]
                            public class MyClass
                            {

                            }
                            """;
        ClassContext sut = new(new SourceCodeFileContext(), _chunk)
        {
            Settings =
            {
                GeneratorAssembly = GetType().Assembly
            }
        };

        //act
        sut.WithGeneratedCodeAttr();

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