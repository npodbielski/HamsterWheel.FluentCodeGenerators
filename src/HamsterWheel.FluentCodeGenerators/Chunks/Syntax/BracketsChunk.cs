using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class BracketsChunk(BracesType bracesType, params ICodeChunk?[] content) : ICodeChunk
{
    protected ICodeChunk Content { get; } = new CompoundChunk(content);

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

public class BracesChunk(params ICodeChunk[] chunks)
    : BracketsChunk(BracesType.Curly, [new EmptyLineChunk(), ..chunks])
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine();
        return base.AppendChunks(stringBuilder);
    }
}

public enum BracesType
{
    Square,
    Round,
    Angle,
    Curly
}