using System.Data;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IFieldTargetExtensionsUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _chunk;

    public IFieldTargetExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        _sut = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithFieldOfT1_WhenCalledStringAnsNameInNamespace_ThenRendersField()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private HttpContext _context;
                                }
                                """;

        //act
        _sut.WithField("_context",
            new NameInNamespace("HttpContext", "Microsoft.AspNetCore.Http.HttpContextExtensions"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithFieldOfT1_WhenCalledStringAnsNameInNamespace_ThenAddsUsing()
    {
        //arrange
        var expected = "using Microsoft.AspNetCore.Http.HttpContextExtensions;";

        //act
        _sut.WithField("_context",
            new NameInNamespace("HttpContext", "Microsoft.AspNetCore.Http.HttpContextExtensions"));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithFieldOfT1_WhenCalledWithStringAndType_ThenRendersField()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private IDbCommand _command;
                                }
                                """;

        //act
        _sut.WithField("command", typeof(IDbCommand));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithFieldOfT1_WhenCalledWithStringAndTypeAndConfigurator_ThenConfiguratorIsUsed()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private static IDbCommand _command;
                                }
                                """;

        //act
        _sut.WithField("command", typeof(IDbCommand), c => c.MakeStatic());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithFieldOfT1_WhenCalledWithStringAndType_ThenAddsUsing()
    {
        //arrange
        var expected = "using System.Data;";

        //act
        _sut.WithField("_context", typeof(IDbCommand));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }
}