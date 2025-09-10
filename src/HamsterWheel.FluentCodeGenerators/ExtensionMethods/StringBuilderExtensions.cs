using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks;

namespace HamsterWheel.FluentCodeGenerators;

public static class StringBuilderExtensions
{
    public static void AppendSingleSpace(this StringBuilder stringBuilder)
    {
        stringBuilder.Append(' ');
    }

    public static StringBuilder AppendChunk<TChunk>(this StringBuilder stringBuilder)
        where TChunk : ICodeChunk, new()
    {
        new TChunk().AppendChunks(stringBuilder);
        return stringBuilder;
    }

    public static StringBuilder AppendChunk<TChunk>(this StringBuilder stringBuilder, TChunk chunk)
        where TChunk : ICodeChunk
    {
        chunk.AppendChunks(stringBuilder);
        return stringBuilder;
    }
}