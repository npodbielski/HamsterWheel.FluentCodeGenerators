using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Exceptions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Syntax;

public class ParameterValueChunkUnitTests
{
    [Fact]
    public void Name_WhenCalledBeforeSettingName_ThenThrows()
    {
        //arrange
        var chunk = new ParameterValueChunk(new PlainValueChunk("1"));
        var action = () => chunk.Name;

        //act
        var actual = action.Should().Throw<NotInitializedChunkException>();

        //assert
        actual.WithMessage("Cannot read property of not initialized chunk");
    }
}