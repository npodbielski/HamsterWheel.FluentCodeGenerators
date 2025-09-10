using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

//TODO: should be rewritten to semicolon chunk and keyword chunk
public class LineWithPrependAndSuffixChunk(string content, string? prefix = null, string? suffix = null)
    : ICodeChunk
{
    public static LineWithPrependAndSuffixChunk WithSemicolon(string content, string? prefix = default)
    {
        return new LineWithPrependAndSuffixChunk(content, prefix: prefix, suffix: ";");
    }

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.Append($"{prefix}{content}{suffix}");;
        return true;
    }
}