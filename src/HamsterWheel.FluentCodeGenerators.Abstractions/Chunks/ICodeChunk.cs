using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public interface ICodeChunk
{
    bool AppendChunks(StringBuilder stringBuilder);
}