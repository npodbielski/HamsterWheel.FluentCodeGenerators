using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ExternAliasChunk(string alias) : ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine($"extern alias {alias};");
        return true;
    }
}