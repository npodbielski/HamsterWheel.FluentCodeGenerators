using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class TypeOfExpressionChunk(string identifier) : BodyChunk
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        new TypeOfKeywordChunk().AppendChunks(stringBuilder);
        new BracketsChunk(BracesType.Round, new PlainValueChunk(identifier)).AppendChunks(stringBuilder);
        return true;
    }
}