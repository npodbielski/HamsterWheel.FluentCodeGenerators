using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IClassContextExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _classChunk;

    public IClassContextExtensionsUnitTests()
    {
        _classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        var context = new SourceCodeFileContext();
        _sut = new(context, _classChunk);
    }

    [Fact]
    public void WithCode_WhenRendered_ThenHaveCustomCode()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    custom code
                                }
                                """;

        //act
        _sut.WithCode("custom code");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenRendered_ThenHaveAttributeApplied()
    {
        //arrange
        const string expected = """
                                [ExcludeFromCodeCoverage]
                                public class MyClass
                                {
                                
                                }
                                """;

        //act
        _sut.WithAttribute<ExcludeFromCodeCoverageAttribute>();

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void ImplementsInterface_WhenRendered_ThenInheritsFromInterface()
    {
        //arrange
        const string expected = """
                                public class MyClass : IQueryable
                                {
                                
                                }
                                """;

        //act
        _sut.ImplementsInterface<IQueryable>();

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenRendered_ThenHaveProp()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public IQueryable Queryable { get; set; }
                                }
                                """;

        //act
        _sut.WithProp<IQueryable>("Queryable");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithMethod_WhenRendered_ThenHaveMethod()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void Get()
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod("Get");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithField_WhenRendered_ThenHaveMethod()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private int _int;
                                }
                                """;

        //act
        _sut.WithField<int>("int");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void ConfigureUsing_WhenRendered_ThenHaveConfiguratorConfiguredCode()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    configurator code
                                }
                                """;

        //act
        _sut.ConfigureUsing<ClassConfigurator>();

        //assert
        _classChunk.Should().RenderAs(expected);
    }
}

public class ClassConfigurator : IContextConfigurator<IClassContext>
{
    public void Configure(IClassContext context) => context.WithCode("configurator code");
}