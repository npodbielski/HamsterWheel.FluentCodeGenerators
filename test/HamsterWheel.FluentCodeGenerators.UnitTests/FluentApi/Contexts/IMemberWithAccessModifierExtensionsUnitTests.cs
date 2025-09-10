using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IMemberWithAccessModifierExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private ClassDefinitionChunk _chunk;

    public IMemberWithAccessModifierExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName());
        _sut = new ClassContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void MakeProtected_WhenCalled_ThenClassIsProtected()
    {
        //arrange
        var expected = """
                       protected class MyClass
                       {
                       
                       }
                       """;

        //act
        _sut.MakeProtected();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakePublic_WhenCalled_ThenClassIsProtected()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                       
                       }
                       """;

        //act
        _sut.MakePublic();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakePrivate_WhenCalled_ThenClassIsProtected()
    {
        //arrange
        var expected = """
                       private class MyClass
                       {
                       
                       }
                       """;

        //act
        _sut.MakePrivate();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakePrivateProtected_WhenCalled_ThenClassIsProtected()
    {
        //arrange
        var expected = """
                       protected private class MyClass
                       {
                       
                       }
                       """;

        //act
        _sut.MakePrivateProtected();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeProtectedInternal_WhenCalled_ThenClassIsProtected()
    {
        //arrange
        var expected = """
                       protected internal class MyClass
                       {
                       
                       }
                       """;

        //act
        _sut.MakeProtectedInternal();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeInternal_WhenCalled_ThenClassIsProtected()
    {
        //arrange
        var expected = """
                       internal class MyClass
                       {
                       
                       }
                       """;

        //act
        _sut.MakeInternal();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}