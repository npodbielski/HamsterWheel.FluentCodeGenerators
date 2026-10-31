using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Enums;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Enums;

public class EnumChunkUnitTests
{
    [Fact]
    public void Name_WhenCalled_ThenReturnName()
    {
        //arrange
        var expected = "MyEnum";
        var chunk = new EnumChunk();

        //act
        var actual = chunk.Name;

        //assert
        actual.ToString().Should().Be(expected);
    }

    [Fact]
    public void Name_WhenCalledOnInterface_ThenReturnName()
    {
        //arrange
        var expected = "MyEnum";
        var chunk = new EnumChunk();

        //act
        var actual = ((INamedChunk)chunk).Name;

        //assert
        actual.ToString().Should().Be(expected);
    }
}