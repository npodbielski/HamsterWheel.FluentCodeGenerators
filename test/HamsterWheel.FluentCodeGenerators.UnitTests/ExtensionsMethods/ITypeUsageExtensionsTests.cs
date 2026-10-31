using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class ITypeUsageExtensionsUnitTests
{
    private readonly ITypeUsageContext _sut;
    private readonly TypeNameChunk _chunk;
    public ITypeUsageExtensionsUnitTests()
    {
        _chunk = new TypeNameChunk("Test");
        _sut = new TypeUsageContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithGenericArgument_WhenCalledWithINamedTypeSymbol_ThenRendersCorrectly()
    {
        //arrange
        var expected = "Test<test>";

        //act
        _sut.WithGenericArgument(new DummyNamedTypeSymbol("test", "testNamespace"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalledWithINameInNamespaceToken_ThenRendersCorrectly()
    {
        //arrange
        var expected = "Test<HttpClient>";

        //act
        _sut.WithGenericArgument(typeof(HttpClient).ToNameInNamespace());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalledWithPascalCaseName_ThenRendersCorrectly()
    {
        //arrange
        var expected = "Test<SomeName>";

        //act
        _sut.WithGenericArgument(new PascalCaseName("SomeName"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void ToNameInNamespace_WhenCalledWithArrayType_ThenReturnsCorrectData()
    {
        //arrange
        var type = typeof(string[]);

        //act
        var result = type.ToNameInNamespace();

        //assert
        result.Name.NameAsString.Should().Be("String");
        result.Namespace.NamespaceAsString.Should().Be("System");
        result.IsArray.Should().BeTrue();
    }

    [Fact]
    public void ToNameInNamespace_WhenCalledWithInt32Array_ThenReturnsCorrectData()
    {
        //arrange
        var type = typeof(int[]);

        //act
        var result = type.ToNameInNamespace();

        //assert
        result.Name.NameAsString.Should().Be("Int32");
        result.Namespace.NamespaceAsString.Should().Be("System");
        result.IsArray.Should().BeTrue();
    }

    [Fact]
    public void ToNameInNamespace_WhenCalledWithNonArrayType_ThenReturnsCorrectData()
    {
        //arrange
        var type = typeof(string);

        //act
        var result = type.ToNameInNamespace();

        //assert
        result.Name.NameAsString.Should().Be("String");
        result.Namespace.NamespaceAsString.Should().Be("System");
        result.IsArray.Should().BeFalse();
    }

    [Fact]
    public void ToNameInNamespace_WhenCalledWithGenericType_ThenReturnsCorrectData()
    {
        //arrange
        var type = typeof(List<int>);

        //act
        var result = type.ToNameInNamespace();

        //assert
        result.Name.NameAsString.Should().Be("List`1");
        result.Namespace.NamespaceAsString.Should().Be("System.Collections.Generic");
        result.IsArray.Should().BeFalse();
    }
}

public class BodyBuilderInterpolatedStringHandlerUnitTests
{
    [Fact]
    public void AppendFormatted_WhenCalledWithArrayTypeToken_ThenAppendsCorrectly()
    {
        //arrange
        var handler = new BodyBuilderInterpolatedStringHandler(0, 1);
        var typeToken = new NameInNamespace(new PascalCaseName("String"), "System", true);

        //act
        handler.AppendFormatted(typeToken);

        //assert
        handler.GetFormattedText().Should().Be("String[]");
        handler.Namespaces.Should().HaveCount(1);
        handler.Namespaces[0].NamespaceAsString.Should().Be("System");
    }

    [Fact]
    public void AppendFormatted_WhenCalledWithNonArrayTypeToken_ThenAppendsCorrectly()
    {
        //arrange
        var handler = new BodyBuilderInterpolatedStringHandler(0, 1);
        var typeToken = new NameInNamespace(new PascalCaseName("String"), "System", false);

        //act
        handler.AppendFormatted(typeToken);

        //assert
        handler.GetFormattedText().Should().Be("String");
        handler.Namespaces.Should().HaveCount(1);
        handler.Namespaces[0].NamespaceAsString.Should().Be("System");
    }

    [Fact]
    public void AppendFormatted_WhenCalledWithInt32ArrayTypeToken_ThenAppendsCorrectly()
    {
        //arrange
        var handler = new BodyBuilderInterpolatedStringHandler(0, 1);
        var typeToken = new NameInNamespace(new PascalCaseName("Int32"), "System", true);

        //act
        handler.AppendFormatted(typeToken);

        //assert
        handler.GetFormattedText().Should().Be("Int32[]");
        handler.Namespaces.Should().HaveCount(1);
        handler.Namespaces[0].NamespaceAsString.Should().Be("System");
    }

    [Fact]
    public void AppendFormatted_WhenCalledWithCodeWithNamespaceChunk_ThenAppendsCorrectly()
    {
        //arrange
        var handler = new BodyBuilderInterpolatedStringHandler(0, 1);
        var token = new NameInNamespace(new PascalCaseName("MyClass"), "MyNamespace");
        var chunk = new NewExpressionChunk(token);

        //act
        handler.AppendFormatted(chunk);

        //assert
        handler.GetFormattedText().Should().Be("new MyClass()");
        handler.Namespaces.Should().HaveCount(1);
        handler.Namespaces[0].NamespaceAsString.Should().Be("MyNamespace");
    }
}