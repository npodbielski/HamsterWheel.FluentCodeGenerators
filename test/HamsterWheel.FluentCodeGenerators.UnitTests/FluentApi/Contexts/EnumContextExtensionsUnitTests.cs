using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Enums;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class EnumContextExtensionsUnitTests
{
    private readonly EnumChunk _chunk;
    private readonly EnumContext _sut;

    public EnumContextExtensionsUnitTests()
    {
        _chunk = new EnumChunk();
        var context = new SourceCodeFileContext();
        _sut = EnumContext.From(context, _chunk);
    }

    [Fact]
    public void Named_WhenRendered_ThenClassHaveCorrectName()
    {
        //arrange

        const string expected = """
                                [ExcludeFromCodeCoverage]
                                public enum MyEnum
                                {
                                }
                                """;

        //act
        _sut.WithAttribute<ExcludeFromCodeCoverageAttribute>();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}