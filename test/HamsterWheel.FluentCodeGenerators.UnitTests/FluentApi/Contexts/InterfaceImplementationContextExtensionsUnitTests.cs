using System.Collections;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class InterfaceImplementationContextExtensionsUnitTests
{
    private readonly InterfaceImplementationContext _sut;
    private readonly TypeNameChunk _chunk;

    public InterfaceImplementationContextExtensionsUnitTests()
    {
        _chunk = TypeNameChunk.From<IEnumerable>();
        _sut = new InterfaceImplementationContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void FromOfT1_WhenCalled_ThenCorrectlySetsType()
    {
        //arrange
        const string expected = "IQueryable";

        //act
        _sut.From<IQueryable>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void FromOfT1_WhenCalled_ThenAddsUsing()
    {
        //arrange
        const string expected = "using System.Linq;";

        //act
        _sut.From<IQueryable>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenCorrectlySetsType()
    {
        //arrange
        const string expected = "IEnumerable<decimal>";

        //act
        _sut.WithGenericArgument<decimal>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenAddsUsing()
    {
        //arrange
        const string expected = "using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;";

        //act
        _sut.From<IClassContext>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }
}