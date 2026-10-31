using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IUsingAppenderExtensionsUnitTests
{
    private readonly ClassDefinitionChunk _chunk;
    private readonly ClassContext _context;

    public IUsingAppenderExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        _context = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithUsings_WhenCalledWithMultipleNamespaces_ThenAddsAllUsings()
    {
        //arrange
        var expected = """
                       using System;
                       using System.Collections.Generic;
                       using System.Linq;
                       """;

        //act
        _context.WithUsings("System", "System.Collections.Generic", "System.Linq");

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithUsings_WhenCalledWithSingleNamespace_ThenAddsSingleUsing()
    {
        //arrange
        var expected = """
                       using System.IO;
                       """;

        //act
        _context.WithUsings("System.IO");

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void AddUsing_WhenCalledWithNameInNamespaceToken_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _context.AddUsing(NameInNamespace.From("DateTime", "System"));

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void AddUsing_WhenCalledWithGeneric_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _context.AddUsing<DateTime>();

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void AddUsing_WhenCalledWithType_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _context.AddUsing(typeof(DateTime));

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void AddUsing_WhenCalledWithString_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System.IO;
                       """;

        //act
        _context.AddUsing("System.IO");

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void AddUsing_WhenCalledWithNamedTypeSymbol_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using TestNamespace;
                       """;
        var namedTypeSymbol = new DummyNamedTypeSymbol("TestType", "TestNamespace");

        //act
        _context.AddUsing(namedTypeSymbol);

        //assert
        _context.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void AddUsing_WhenCalledWithMultipleTypes_ThenAddsAllUsings()
    {
        //arrange
        var expected = """
                       using System;
                       using System.Collections.Generic;
                       using System.IO;
                       """;

        //act
        _context.AddUsing<DateTime>();
        _context.AddUsing<List<int>>();
        _context.AddUsing(typeof(File));

        //assert
        _context.Usings.Should().RenderAs(expected);
    }
}
