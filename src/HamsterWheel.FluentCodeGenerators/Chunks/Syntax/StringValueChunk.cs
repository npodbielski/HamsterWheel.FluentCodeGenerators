using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class StringValueChunk(string value) : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (value.Contains("\n") || value.Contains("\""))
        {
            stringBuilder.Append(value.Trim().TripleQuote());
        }
        else
        {
            stringBuilder.Append(value.Trim().Quote());
        }

        return true;
    }
}