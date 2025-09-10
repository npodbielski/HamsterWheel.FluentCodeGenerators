using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class NameOfExpressionChunk(string identifier) : BodyChunk
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        new NameOfKeywordChunk().AppendChunks(stringBuilder);
        new BracketsChunk(BracesType.Round, new PlainValueChunk(identifier)).AppendChunks(stringBuilder);
        return true;
    }
}