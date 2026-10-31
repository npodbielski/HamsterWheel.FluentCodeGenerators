using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public interface ICodeWithNamespaceChunk : ICodeChunk
{
    INamespace[] Namespace { get; }
}