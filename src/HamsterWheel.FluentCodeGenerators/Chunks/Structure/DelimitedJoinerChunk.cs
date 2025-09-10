using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class DelimitedJoinerChunk(Delimiter delimiter, IEnumerable<ICodeChunk>? codeChunks) : ICodeChunk
{
    public virtual bool AppendChunks(StringBuilder stringBuilder)
    {
        if (codeChunks is not null)
        {
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

            if (chunks.Length > 0)
            {
                return true;
            }
        }

        return false;
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

public record Delimiter
{
    private Delimiter(string? delimiterString, int? numberOfNewLines)
    {
        DelimiterString = delimiterString;
        NumberOfNewLines = numberOfNewLines;
        if (delimiterString is null && numberOfNewLines is null)
        {
            throw new ArgumentException(
                $"Either '{nameof(delimiterString)}' or '{nameof(numberOfNewLines)}' parameter needs to be set!");
        }
    }

    public bool IsNewLine()
    {
        return NumberOfNewLines is not null;
    }

    public static implicit operator Delimiter(string delimiterString)
    {
        return new Delimiter(delimiterString, numberOfNewLines: null);
    }

    public static Delimiter SingleNewLine => new(null, 1);
    public static Delimiter DoubleNewLine => new(null, 2);
    public static Delimiter Empty => new("");

    public string? DelimiterString { get; }
    public int? NumberOfNewLines { get; }

    public void Deconstruct(out string? delimiter, out int? numberOfNewLines)
    {
        delimiter = DelimiterString;
        numberOfNewLines = NumberOfNewLines;
    }
}