using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class BracketsChunk(BracesType bracesType, params ICodeChunk?[] content) : ICodeChunk
{
    private ICodeChunk Content { get; } = new CompoundChunk(content);

    public virtual bool AppendChunks(StringBuilder stringBuilder)
    {
        AppendOpening(stringBuilder);
        Content.AppendChunks(stringBuilder);
        AppendClosing(stringBuilder);

        return true;
    }

    private void AppendOpening(StringBuilder stringBuilder)
    {
        stringBuilder.Append(bracesType switch
        {
            BracesType.Square => '[',
            BracesType.Round => '(',
            BracesType.Angle => '<',
            BracesType.Curly => '{',
            _ => throw new ArgumentOutOfRangeException(nameof(bracesType), bracesType, null)
        });
    }

    private void AppendClosing(StringBuilder stringBuilder)
    {
        stringBuilder.Append(bracesType switch
        {
            BracesType.Square => ']',
            BracesType.Round => ')',
            BracesType.Angle => '>',
            BracesType.Curly => '}',
            _ => throw new ArgumentOutOfRangeException(nameof(bracesType), bracesType, null)
        });
    }
}