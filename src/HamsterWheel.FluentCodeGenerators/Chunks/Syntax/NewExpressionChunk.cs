using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class NewExpressionChunk : BodyChunk
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        new NewKeywordChunk().AppendChunks(stringBuilder);
        new BracketsChunk(BracesType.Round).AppendChunks(stringBuilder);
        return true;
    }
}