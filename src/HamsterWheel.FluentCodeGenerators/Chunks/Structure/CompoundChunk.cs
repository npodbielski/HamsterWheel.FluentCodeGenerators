using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class CompoundChunk(IEnumerable<ICodeChunk?> codeChunks) : ICodeChunk
{
    public virtual bool AppendChunks(StringBuilder stringBuilder)
    {
        var added = false;
        foreach (var chunk in codeChunks)
        {
            added |= chunk?.AppendChunks(stringBuilder) == true;
        }

        return added;
    }
}