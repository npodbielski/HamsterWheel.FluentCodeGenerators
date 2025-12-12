using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ImplementationsWithPrimaryCtorContextUnitTests
{
    private readonly ImplementationsWithPrimaryCtorContext<ClassContext> _sut;
    private readonly ImplementationsListChunk _chunk;

    public ImplementationsWithPrimaryCtorContextUnitTests()
    {
        _chunk = new ImplementationsListChunk();
        _sut = new ImplementationsWithPrimaryCtorContext<ClassContext>(
            new ClassContext(new SourceCodeFileContext(),
                new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClassName"))), _chunk);
    }

    [Fact]
    public void ParametersNames_WhenCalledClassHaveNoParameters_ThenEmpty() => _sut.ParametersNames.Should().BeEmpty();

    [Fact]
    public void From_WhenCalled_ThenSetsBaseType()
    {
        //arrange
        const string expected = ": BaseClass";

        //act
        _sut.From("BaseClass".ToPascalCaseName(), "CustomNamespace".ToNamespace());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalled_ThenAddsUsing()
    {
        //arrange
        const string expected = "using CustomNamespace;";

        //act
        _sut.From("BaseClass".ToPascalCaseName(), "CustomNamespace".ToNamespace());

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenSetsBaseType()
    {
        //arrange
        const string expected = ": object<Argument>";

        //act
        _sut.WithGenericArgument(gat => gat.From("Argument".ToPascalCaseName(), "CustomNamespace".ToNamespace()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenAddsUsing()
    {
        //arrange
        const string expected = "using CustomNamespace;";

        //act
        _sut.WithGenericArgument(gat => gat.From("Argument".ToPascalCaseName(), "CustomNamespace".ToNamespace()));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCtorCall_WhenCalled_ThenAddsCtor()
    {
        //arrange
        const string expected = ": object()";

        //act
        _sut.WithCtorCall(_ => { });

        //assert
        _chunk.Should().RenderAs(expected);
    }
}