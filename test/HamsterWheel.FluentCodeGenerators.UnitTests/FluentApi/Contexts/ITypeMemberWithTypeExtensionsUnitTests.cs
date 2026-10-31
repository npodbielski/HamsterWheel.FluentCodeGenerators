using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ITypeMemberWithTypeExtensionsUnitTests
{
    private readonly PropertyDefinitionChunk _chunk;
    private readonly IPropertyContext _sut;

    public ITypeMemberWithTypeExtensionsUnitTests()
    {
        _chunk = new PropertyDefinitionChunk("MyProperty", TypeNameChunk.String);
        var context = new ClassContext(new SourceCodeFileContext(), new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass")));
        _sut = PropertyContext.From(context, _chunk);
    }

    [Fact]
    public void OfType_WhenCalledWithNameInNamespace_ThenSetsTypeCorrectly()
    {
        //arrange
        var expected = "public int MyProperty { get; set; }";

        //act
        _sut.OfType(NameInNamespace.From("int", "System"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithNameInNamespace_WithConfigure_ThenSetsTypeAndConfigures()
    {
        //arrange
        var expected = "public int? MyProperty { get; set; }";

        //act
        _sut.OfType(NameInNamespace.From("int", "System"), t => t.MakeNullable());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithType_ThenSetsTypeCorrectly()
    {
        //arrange
        var expected = "public decimal MyProperty { get; set; }";

        //act
        _sut.OfType(typeof(decimal));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithType_WithConfigure_ThenSetsTypeAndConfigures()
    {
        //arrange
        var expected = "public decimal[] MyProperty { get; set; }";

        //act
        _sut.OfType(typeof(decimal), t => t.MakeArray());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithString_ThenSetsTypeCorrectly()
    {
        //arrange
        var expected = "public DateTime MyProperty { get; set; }";

        //act
        _sut.OfType("DateTime");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithString_WithConfigure_ThenSetsTypeAndConfigures()
    {
        //arrange
        var expected = "public DateTime? MyProperty { get; set; }";

        //act
        _sut.OfType("DateTime", t => t.MakeNullable());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithNamedTypeSymbol_ThenSetsTypeCorrectly()
    {
        //arrange
        var expected = "public TestType MyProperty { get; set; }";
        var namedTypeSymbol = new DummyNamedTypeSymbol("TestType", "TestNamespace");

        //act
        _sut.OfType(namedTypeSymbol);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalledWithNamedTypeSymbol_WithConfigure_ThenSetsTypeAndConfigures()
    {
        //arrange
        var expected = "public TestType[] MyProperty { get; set; }";
        var namedTypeSymbol = new DummyNamedTypeSymbol("TestType", "TestNamespace");

        //act
        _sut.OfType(namedTypeSymbol, t => t.MakeArray());

        //assert
        _chunk.Should().RenderAs(expected);
    }
}