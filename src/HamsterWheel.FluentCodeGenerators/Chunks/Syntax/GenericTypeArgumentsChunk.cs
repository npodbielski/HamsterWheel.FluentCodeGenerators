using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class GenericTypeArgumentsChunk : ICodeChunk
{
    internal readonly List<TypeNameChunk> Types = [];

    public void AddArg(TypeNameChunk chunk) => Types.Add(chunk);
    
    public bool AppendChunks(StringBuilder stringBuilder)
    {
        return Types.Any() &&
               new BracketsChunk(BracesType.Angle, new CommaDelimitedJoinChunk(codeChunks: Types)).AppendChunks(
                   stringBuilder);
    }
}