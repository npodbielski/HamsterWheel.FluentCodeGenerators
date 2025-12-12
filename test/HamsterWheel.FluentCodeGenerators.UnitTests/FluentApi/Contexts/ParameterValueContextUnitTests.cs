using System.Globalization;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ParameterValueContextUnitTests
{
    private readonly ParameterValueContext _sut;
    private readonly ParameterValueChunk _chunk;

    public ParameterValueContextUnitTests()
    {
        _chunk = ParameterValueChunk.From("2", FluentApiSettings.DefaultCulture);
        _sut = new ParameterValueContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void MakeNamed_WhenCalled_ThenParameterIsNamed()
    {
        //arrange
        var expected = "myParam: 2";

        //act
        _sut.MakeNamed("myParam");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseStringValue_WhenCalled_ThenParameterIsNamed()
    {
        //arrange
        var expected = "\"test\"";

        //act
        _sut.UseStringValue("test");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalled_ThenParameterHaveCorrectValue()
    {
        //arrange
        var expected = "33232321321";

        //act
        _sut.UseExpression("33232321321");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void FromConst_WhenCalled_ThenParameterIsNamed()
    {
        //arrange
        var expected = "3213232312.123123";

        //act
        _sut.UseConst(3213232312.123123);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void FromConst_WhenCultureChangedInSettings_ThenUsesThisCultureToConvertToString()
    {
        //arrange
        var expected = "3213232312,123123";
        _sut.Settings.DefaultCulture = new CultureInfo("pl-PL");

        //act
        _sut.UseConst(3213232312.123123);

        //assert
        _chunk.Should().RenderAs(expected);
    }
}