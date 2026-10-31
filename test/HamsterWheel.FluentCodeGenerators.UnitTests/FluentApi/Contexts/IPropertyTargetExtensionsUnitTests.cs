using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class IPropertyTargetExtensionsUnitTests
{
    private ClassContext _sut;
    private readonly ClassDefinitionChunk _chunk;

    public IPropertyTargetExtensionsUnitTests()
    {
        _chunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        _sut = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithCollectionProp_WhenCalled_ThenAddsProperty()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public IEnumerable<decimal> Collection { get; set; }
                       }
                       """;

        //act
        _sut.WithCollectionProp("collection", typeof(IEnumerable<>), typeof(decimal));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCollectionProp_WhenCalledAndNullabilityEnabled_ThenDisableNullabilityWarning()
    {
        //arrange
        var context = new SourceCodeFileContext();
        context.EnableNullability();
        _sut = new(context, _chunk);
        var expected = """
                       public class MyClass
                       {
                           public IEnumerable<decimal> Collection { get; set; } = default!;
                       }
                       """;

        //act
        _sut.WithCollectionProp("collection", typeof(IEnumerable<>), typeof(decimal));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPropOfT1_WhenCalledWithStringAndNameInNamespaceToken_ThenAddsProperty()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public DateTime DateTime { get; set; }
                       }
                       """;

        //act
        _sut.WithProp("DateTime", NameInNamespace.From("DateTime", "System"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPropOfT1_WhenCalledWithStringAndNameInNamespaceToken_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _sut.WithProp("DateTime", NameInNamespace.From("DateTime", "System"));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPropOfT1_WhenCalledWithStringAndType_ThenAddsProperty()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public DateOnly DateTime { get; set; }
                       }
                       """;

        //act
        _sut.WithProp("DateTime", typeof(DateOnly));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPropOfT1_WhenCalledWithStringAndType_ThenAddsUsings()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _sut.WithProp("DateTime", typeof(DateOnly));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPropOfT1_WhenCalledWith2Strings_ThenAddsProperty()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public DateTimeOffset DateTime { get; set; }
                       }
                       """;

        //act
        _sut.WithProp("DateTime", "DateTimeOffset");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPropOfT1_WhenCalledWith1String_ThenAddsProperty()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public string SomeString { get; set; }
                       }
                       """;

        //act
        _sut.WithProp("SomeString");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenCalledWithNamedTypeSymbol_ThenAddsProperty()
    {
        //arrange
        var namedTypeSymbol = new DummyNamedTypeSymbol("TestType", "TestNamespace");
        var expected = """
                       public class MyClass
                       {
                           public TestType TestProperty { get; set; }
                       }
                       """;

        //act
        _sut.WithProp("TestProperty", namedTypeSymbol);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenCalledWithNamedTypeSymbolAndConfigure_ThenAddsPropertyAndConfigures()
    {
        //arrange
        var namedTypeSymbol = new DummyNamedTypeSymbol("TestType", "TestNamespace");
        var expected = """
                       public class MyClass
                       {
                           public TestType TestProperty { get; set; } = new();
                       }
                       """;

        //act
        _sut.WithProp("TestProperty", namedTypeSymbol, p => p.WithInitializer<NewExpressionChunk>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenCalledWithNameInNamespaceTokenAndConfigure_ThenAddsPropertyAndConfigures()
    {
        //arrange
        var typeToken = NameInNamespace.From("DateTime", "System");
        var expected = """
                       public class MyClass
                       {
                           public DateTime DateTime { get; set; } = new();
                       }
                       """;

        //act
        _sut.WithProp("DateTime", typeToken, p => p.WithInitializer<NewExpressionChunk>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenCalledWithTypeAndConfigure_ThenAddsPropertyAndConfigures()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public DateOnly DateTime { get; set; } = new();
                       }
                       """;

        //act
        _sut.WithProp("DateTime", typeof(DateOnly), p => p.WithInitializer<NewExpressionChunk>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenCalledWithStringAndConfigure_ThenAddsPropertyAndConfigures()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public DateTimeOffset DateTime { get; set; } = new();
                       }
                       """;

        //act
        _sut.WithProp("DateTime", "DateTimeOffset", p => p.WithInitializer<NewExpressionChunk>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenCalledWithConfigureOnly_ThenAddsPropertyAndConfigures()
    {
        //arrange
        var expected = """
                       public class MyClass
                       {
                           public string SomeString { get; set; } = new();
                       }
                       """;

        //act
        _sut.WithProp("SomeString", p => p.WithInitializer<NewExpressionChunk>());

        //assert
        _chunk.Should().RenderAs(expected);
    }
}