using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public interface INamedChunk : ICodeChunk
{
    public IName Name { get; }
}