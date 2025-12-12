using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.External;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.External;

public class ClrTypeInfoUnitTests
{
    [Fact]
    public void Type_WhenRetrieved_ThenHaveCorrectValue()
    {
        //arrange
        var expected = typeof(int);
        var sut = new ClrTypeInfo(expected);

        //act
        var actual = sut.Type;

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void IsNullable_WhenRetrieved_ThenHaveCorrectValue()
    {
        //arrange
        var expected = typeof(int);
        var sut = new ClrTypeInfo(expected);

        //act
        var actual = sut.IsNullable;

        //assert
        actual.Should().Be(false);
    }

    [Fact]
    public void ImplicitCastFromType_WhenDone_ThenHaveCorrectValue()
    {   
        //arrange
        var expected = typeof(int);

        //act
        ClrTypeInfo actual = expected;

        //assert
        actual.Type.Should().Be(expected);
    }
}