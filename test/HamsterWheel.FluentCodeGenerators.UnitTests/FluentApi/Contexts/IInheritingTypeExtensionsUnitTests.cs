using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IInheritingTypeExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _chunk;

    public IInheritingTypeExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        _sut = new ClassContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithBase_WhenCalledWithSymbol_ThenInheritsFromThisBase()
    {
        //arrange
        var expected = """
                       public class MyClass : TestTypeSymbol
                       {
                       
                       }
                       """;

        //act
        _sut.WithBase(new DummyNamedTypeSymbol("TestTypeSymbol", "ImportedNamespace"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenCalledWithSymbol_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using ImportedNamespace;
                       """;

        //act
        _sut.WithBase(new DummyNamedTypeSymbol("TestTypeSymbol", "ImportedNamespace"));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenCalledWithNameInNamespace_ThenInheritsFromThisBase()
    {
        //arrange
        var expected = """
                       public class MyClass : TestClass
                       {
                       
                       }
                       """;

        //act
        _sut.WithBase(NameInNamespace.From("TestClass", "ExternalNamespace"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenCalledWithNameInNamespace_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using ExternalNamespace;
                       """;

        //act
        _sut.WithBase(NameInNamespace.From("TestClass", "ExternalNamespace"));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenCalledWith2Strings_ThenInheritsFromThisBase()
    {
        //arrange
        var expected = """
                       public class MyClass : BaseType
                       {
                       
                       }
                       """;

        //act
        _sut.WithBase("BaseType", "ExternalNamespace");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenCalledWith2Strings_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using SomeNamespace;
                       """;

        //act
        _sut.WithBase("BaseType", "SomeNamespace");

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }
}