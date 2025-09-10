using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public class FileScopeNamespaceChunk(INamespace @namespace) : ICodeChunk
{
    private readonly ICodeChunk? _namespaceChunk =
        LineWithPrependAndSuffixChunk.WithSemicolon(@namespace.ToString(), prefix: "namespace ");

    public static FileScopeNamespaceChunk From(INamespace @namespace) => new(@namespace);

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        _namespaceChunk?.AppendChunks(stringBuilder);
        return true;
    }
}