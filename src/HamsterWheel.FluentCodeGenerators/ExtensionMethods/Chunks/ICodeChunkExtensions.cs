using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public static class ICodeChunkExtensions
{
    public static string Build(this ICodeChunk chunk)
    {
        var stringBuilder = new StringBuilder();
        if (chunk is MethodBodyChunks methodBodyChunks)
        {
            methodBodyChunks.AppendChunks(stringBuilder);
        }
        else
        {
            chunk.AppendChunks(stringBuilder);
        }

        return stringBuilder.ToString();
    }
}