using HamsterWheel.FluentCodeGenerators.Chunks.Body;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Body;

public class AppendableChunkUnitTests
{
    [Fact]
    public void Append_WhenCalledFirstTime_ThenAddsChunks()
    {
        //arrange
        var expected = "test";
        var chunk = new AppendableChunk();

        //act
        chunk.Append("test");

        //assert
        chunk.Should().RenderAs(expected);
    }
}