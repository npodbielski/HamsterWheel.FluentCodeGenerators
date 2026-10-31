using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks;

namespace HamsterWheel.FluentCodeGenerators;

public static class StringBuilderExtensions
{
    extension(StringBuilder stringBuilder)
    {
        public void AppendSingleSpace() => stringBuilder.Append(' ');

        public StringBuilder AppendChunk<TChunk>()
            where TChunk : ICodeChunk, new()
        {
            new TChunk().AppendChunks(stringBuilder);
            return stringBuilder;
        }

        public StringBuilder AppendChunk<TChunk>(TChunk chunk)
            where TChunk : ICodeChunk
        {
            chunk.AppendChunks(stringBuilder);
            return stringBuilder;
        }
    }
}