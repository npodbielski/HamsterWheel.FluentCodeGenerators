using System.Collections;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class InterfaceImplementationContextUnitTests
{
    private readonly InterfaceImplementationContext _sut;
    private readonly TypeNameChunk _chunk;

    public InterfaceImplementationContextUnitTests()
    {
        _chunk = TypeNameChunk.From<IEnumerable>();
        _sut = new InterfaceImplementationContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void From_WhenCalledWithNameAndNamespace_ThenRendersCorrectly()
    {
        //arrange
        const string expected = "IQueryable";

        //act
        _sut.From("IQueryable".ToPascalCaseName(), "System.Linq".ToNamespace());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalledWithNameAndNamespace_ThenAddsUsing()
    {
        //arrange
        const string expected = "using System.Linq;";

        //act
        _sut.From("IQueryable".ToPascalCaseName(), "System.Linq".ToNamespace());

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalledWithNameAndNamespace_ThenRendersCorrectly()
    {
        //arrange
        const string expected = "IEnumerable<decimal>";

        //act
        _sut.WithGenericArgument(gta => gta.From<decimal>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalledWithNameAndNamespace_ThenAddsUsing()
    {
        //arrange
        const string expected = """
                                using System.Linq;
                                """;

        //act
        _sut.From("IQueryable".ToPascalCaseName(), "System.Linq".ToNamespace());

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }
}