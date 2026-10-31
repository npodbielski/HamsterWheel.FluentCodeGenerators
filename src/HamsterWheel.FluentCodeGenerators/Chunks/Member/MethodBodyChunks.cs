using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class MethodBodyChunks : AppendableChunk
{
    //this cannot be overridden since base method is used inside bia BracesChunk -> IndentedChunk -> AppendableChunk
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        new BracesChunk(new IndentedChunk([.. Chunks])).AppendChunks(stringBuilder);

        return Chunks.Count > 0;
    }
}