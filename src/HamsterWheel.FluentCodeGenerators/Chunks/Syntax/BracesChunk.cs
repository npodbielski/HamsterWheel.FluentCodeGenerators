using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class BracesChunk(params ICodeChunk[] chunks)
    : BracketsChunk(BracesType.Curly, [new EmptyLineChunk(), .. chunks])
{
    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine();
        return base.AppendChunks(stringBuilder);
    }
}