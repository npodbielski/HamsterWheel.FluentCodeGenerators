using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class PropertyContextUnitTests
{
    private PropertyContext _sut;
    private readonly PropertyDefinitionChunk _chunk;

    public PropertyContextUnitTests()
    {
        _chunk = PropertyDefinitionChunk.From("Prop0");
        _sut = new(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void Named_WhenCalled_ThenChangesName()
    {
        //arrange
        var expected = "public string NewProp { get; set; }";

        //act
        _sut.Named("newProp");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalled_ThenChangesType()
    {
        //arrange
        var expected = "public DateTime Prop0 { get; set; }";

        //act
        _sut.OfType(t => t.From<DateTime>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalled_ThenAddsUsing()
    {
        //arrange
        var expected = "using System;";

        //act
        _sut.OfType(t => t.From<DateTime>());

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeGetOnly_WhenCalled_ThenNoSetter()
    {
        //arrange
        var expected = "public string Prop0 { get; }";

        //act
        _sut.MakeGetOnly();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeStatic_WhenCalled_ThenMakesStatic()
    {
        //arrange
        var expected = "public static string Prop0 { get; set; }";

        //act
        _sut.MakeStatic();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeOverride_WhenCalled_ThenChangesType()
    {
        //arrange
        var expected = "public override string Prop0 { get; set; }";

        //act
        _sut.MakeOverride();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeOverride_WhenCalledOnStatic_ThenThrows()
    {
        //arrange
        var sut = _sut.MakeStatic();
        var action = () => sut.MakeOverride();

        //act
        var actual = action.Should().Throw<InvalidOperationException>();

        //assert
        actual.WithMessage("Static property cannot be override.");
    }

    [Fact]
    public void MakeSealed_WhenCalled_ThenChangesType()
    {
        //arrange
        var expected = "public sealed override string Prop0 { get; set; }";

        //act
        _sut.MakeSealed();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeNullable_WhenCalled_ThenChangesType()
    {
        //arrange
        var expected = "public string? Prop0 { get; set; }";

        //act
        _sut.MakeNullable();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void DisableNullabilityWarning_WhenCalledAndNoNullabilityIsEnabled_ThenDoesNotChangeAnything()
    {
        //arrange
        var expected = "public string Prop0 { get; set; }";

        //act
        _sut.DisableNullabilityWarning();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void DisableNullabilityWarning_WhenCalledAndNullabilityIsEnabled_ThenAddsNullInitializer()
    {
        //arrange
        var expected = "public string Prop0 { get; set; } = default!;";
        var contextBase = new SourceCodeFileContext();
        contextBase.EnableNullability();
        _sut = new(contextBase, _chunk);

        //act
        _sut.DisableNullabilityWarning();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInitializer_WhenCalled_ThenAddsInitializer()
    {
        //arrange
        var expected = "public string Prop0 { get; set; } = \"Prop0\";";
        var contextBase = new SourceCodeFileContext();
        contextBase.EnableNullability();
        _sut = new(contextBase, _chunk);

        //act
        _sut.WithInitializer(i => i.Append(new PlainValueChunk("Prop0".Quote())));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithInitializer_WhenCalledWithChunk_ThenAddsInitializer()
    {
        //arrange
        var expected = "public string Prop0 { get; set; } = \"Prop0\";";
        var contextBase = new SourceCodeFileContext();
        contextBase.EnableNullability();
        _sut = new(contextBase, _chunk);

        //act
        _sut.WithInitializer(new PlainValueChunk("Prop0".Quote()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeComputed_WhenCalled_ThenPropertyIsComputed()
    {
        //arrange
        var expected = "public string Prop0 => default;";
        var contextBase = new SourceCodeFileContext();
        contextBase.EnableNullability();
        _sut = new(contextBase, _chunk);

        //act
        _sut.MakeComputed();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithExpressionBody_WhenCalled_ThenPropertyIsComputed()
    {
        //arrange
        var expected = "public string Prop0 => \"test\";";
        var contextBase = new SourceCodeFileContext();
        contextBase.EnableNullability();
        _sut = new(contextBase, _chunk);

        //act
        _sut.WithExpressionBody(b => b.Append("test".Quote()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithSetExpressionBody_WhenCalled_ThenSetterIsExpressionBody()
    {
        //arrange
        var expected = """
                       public string Prop0
                       {
                           get => 
                           set => this._prop0 = value;
                       }
                       """;

        //act
        _sut.WithSetExpressionBody(b => b.Append("this._prop0 = value"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WhenComputedWithExpressionBody_ThenPropertyIsComputed()
    {
        //arrange
        var expected = "public string Prop0 => \"test\";";
        var contextBase = new SourceCodeFileContext();
        contextBase.EnableNullability();
        _sut = new(contextBase, _chunk);

        //act
        _sut.MakeComputed().WithExpressionBody(b => b.Append("test".Quote()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData(MemberVisibility.Internal, "internal")]
    [InlineData(MemberVisibility.Private, "private")]
    [InlineData(MemberVisibility.PrivateProtected, "protected private")]
    [InlineData(MemberVisibility.Protected, "protected")]
    [InlineData(MemberVisibility.ProtectedInternal, "protected internal")]
    [InlineData(MemberVisibility.Public, "public")]
    public void SetVisibility_WhenRendered_ThenHaveCorrectVisibilityModifier(MemberVisibility visibility,
        string expectedVisibility)
    {
        //arrange
        var expected = $"{expectedVisibility} string Prop0 {{ get; set; }}";

        //act
        _sut.SetVisibility(visibility);

        //assert
        _chunk.Should().RenderAs(expected);
    }
}