using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class EmptyLineChunk : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine();

        return true;
    }
}