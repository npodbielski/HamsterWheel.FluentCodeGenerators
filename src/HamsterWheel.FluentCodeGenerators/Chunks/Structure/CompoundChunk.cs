using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class CompoundChunk(IEnumerable<ICodeChunk?> codeChunks) : ICodeChunk
{
    public virtual bool AppendChunks(StringBuilder stringBuilder) => codeChunks.Aggregate(false,
        (current, chunk) => current | chunk?.AppendChunks(stringBuilder) == true);
}