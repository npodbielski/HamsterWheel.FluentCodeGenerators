using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class CommentChunk(string comment) : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.Append("/// " + comment);
        return true;
    }
}