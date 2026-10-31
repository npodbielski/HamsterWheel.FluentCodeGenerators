using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class DelimitedJoinerChunk(Delimiter delimiter, IEnumerable<ICodeChunk>? codeChunks) : ICodeChunk
{
    public virtual bool AppendChunks(StringBuilder stringBuilder)
    {
        if (codeChunks is null)
        {
            return false;
        }

        var lastChunkProduced = false;
        var chunks = codeChunks.Where(c => c is not null).ToArray();
        for (var index = 0; index < chunks.Length; index++)
        {
            var chunk = chunks[index];

            if (lastChunkProduced && index <= chunks.Length - 1)
            {
                AddDelimiter(stringBuilder);
            }

            lastChunkProduced = chunk.AppendChunks(stringBuilder);
        }

        return chunks.Length > 0;
    }

    private void AddDelimiter(StringBuilder stringBuilder)
    {
        if (delimiter.IsNewLine())
        {
            for (var i = 0; i < delimiter.NumberOfNewLines; i++)
            {
                stringBuilder.AppendLine();
            }
        }
        else
        {
            stringBuilder.Append(delimiter.DelimiterString);
        }
    }
}