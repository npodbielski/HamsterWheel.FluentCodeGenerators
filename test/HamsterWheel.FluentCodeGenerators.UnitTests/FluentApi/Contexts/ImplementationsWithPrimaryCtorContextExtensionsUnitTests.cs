using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ImplementationsWithPrimaryCtorContextExtensionsUnitTests
{
    private readonly ImplementationsWithPrimaryCtorContext<ClassContext> _sut;
    private readonly ImplementationsListChunk _chunk;

    public ImplementationsWithPrimaryCtorContextExtensionsUnitTests()
    {
        _chunk = new ImplementationsListChunk();
        _sut = new ImplementationsWithPrimaryCtorContext<ClassContext>(
            new ClassContext(new SourceCodeFileContext(),
                new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClassName"))), _chunk);
    }

    [Fact]
    public void From_WhenCalled_ThenSetsBaseType()
    {
        //arrange
        const string expected = ": Attribute";

        //act
        _sut.From<Attribute>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalled_ThenAddsUsing()
    {
        //arrange
        const string expected = "using System;";

        //act
        _sut.From<Attribute>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenSetsBaseType()
    {
        //arrange
        const string expected = ": object<decimal>";

        //act
        _sut.WithGenericArgument<decimal>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenAddsUsing()
    {
        //arrange
        const string expected = "using System;";

        //act
        _sut.WithGenericArgument<decimal>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }
}