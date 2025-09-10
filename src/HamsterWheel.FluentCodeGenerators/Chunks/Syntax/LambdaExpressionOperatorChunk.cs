using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class LambdaExpressionOperatorChunk : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.Append("=>");

        return true;
    }
}