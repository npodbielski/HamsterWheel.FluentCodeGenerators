using System.Runtime.InteropServices;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ParameterDefinitionContextUnitTests
{
    private readonly ParameterDefinitionContext _sut;
    private readonly ParameterDefinitionChunk _chunk;

    public ParameterDefinitionContextUnitTests()
    {
        _chunk = new ParameterDefinitionChunk();
        _sut = new ParameterDefinitionContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithAttribute_WhenCalledWithAttributes_ThenSetsAppliesAttributeToParameter()
    {
        //arrange
        var expected = "[Optional]string stringParam";

        //act
        _sut.WithAttribute(a => a.From<OptionalAttribute>());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Named_WhenCalled_ThenSetsNameOfParameter()
    {
        //arrange
        var expected = "string newName";

        //act
        _sut.Named("newName");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeNullable_WhenCalled_ThenSetsNameOfParameter()
    {
        //arrange
        var expected = "string? stringParam";

        //act
        _sut.MakeNullable();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeRef_WhenCalled_ThenSetsAsRefParameter()
    {
        //arrange
        var expected = "ref string stringParam";

        //act
        _sut.MakeRef();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeOut_WhenCalled_ThenAsOutParameter()
    {
        //arrange
        var expected = "out string stringParam";

        //act
        _sut.MakeOut();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeOut_WhenCalledAfterMakeRef_ThenOverridesIt()
    {
        //arrange
        var expected = "out string stringParam";
        _sut.MakeRef();

        //act
        _sut.MakeOut();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeRef_WhenCalledAfterMakeRef_ThenOverridesIt()
    {
        //arrange
        var expected = "ref string stringParam";
        _sut.MakeOut();

        //act
        _sut.MakeRef();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void OfType_WhenCalled_ThenSetsTypeOfParameter()
    {
        //arrange
        var expected = "int intParam";

        //act
        _sut.OfType<int>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void PushToEnd_WhenCalled_ThenThisParameterOrderIs9999()
    {
        //arrange
        //act
        _sut.PushToEnd();

        //assert
        _chunk.Order.Should().Be(9999);
    }
}