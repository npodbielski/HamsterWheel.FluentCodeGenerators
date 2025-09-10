using System.Text;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public interface ICodeChunk
{
    public bool AppendChunks(StringBuilder stringBuilder);
}

public interface INamedChunk : ICodeChunk
{
    public IName Name { get; }
}