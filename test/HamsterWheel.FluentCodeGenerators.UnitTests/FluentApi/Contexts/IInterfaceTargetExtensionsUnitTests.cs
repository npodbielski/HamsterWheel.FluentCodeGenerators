using System.Data.Common;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IInterfaceTargetExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _chunk;

    public IInterfaceTargetExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        _sut = new ClassContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithInterfaceOfT_WhenCalledWithNameInNamespace_ThenClassInheritsFromInterface()
    {
        //arrange
        var expected = """
                       public class MyClass : IMyInterface
                       {
                       
                       }
                       """;

        //act
        _sut.ImplementsInterface(new NameInNamespace("IMyInterface".ToPascalCaseName(), "MyNamespace".ToNamespace()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInterfaceOfT_WhenCalledWithNameInNamespace_ThenAddsNamespaceToUsings()
    {
        //arrange
        var expected = """
                       using MyNamespace;
                       """;

        //act
        _sut.ImplementsInterface(new NameInNamespace("IMyInterface".ToPascalCaseName(), "MyNamespace".ToNamespace()));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInterfaceOfT_WhenCalledWithType_ThenClassInheritsFromInterface()
    {
        //arrange
        var expected = """
                       public class MyClass : IEquatable<int>
                       {
                       
                       }
                       """;

        //act
        _sut.ImplementsInterface(typeof(IEquatable<int>));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInterfaceOfT_WhenCalledWithType_ThenAddsNamespaceToUsings()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _sut.ImplementsInterface(typeof(IEquatable<int>));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInterfaceOfT_WhenCalledWithTypeThatIsNotInterface_ThenThrows()
    {
        //arrange
        var action = () => _sut.ImplementsInterface(typeof(DbConnection)); 

        //act
        var exception = action.Should().Throw<ArgumentException>();

        //assert
        exception.WithMessage($"Type of {typeof(DbConnection).FullName} is not an interface type!");
    }

    [Fact]
    public void WithInterfaceOfT_WhenCalledWithString_ThenThrows()
    {
        //arrange
        var expected = """
                       public class MyClass : IMyInterface
                       {

                       }
                       """;

        //act
        _sut.ImplementsInterface("IMyInterface");

        //assert
        _chunk.Should().RenderAs(expected);
    }
}