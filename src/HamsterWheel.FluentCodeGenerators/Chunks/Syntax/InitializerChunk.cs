using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class InitializerChunk(string value) : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.Append($"= {value};");
        return true;
    }
}