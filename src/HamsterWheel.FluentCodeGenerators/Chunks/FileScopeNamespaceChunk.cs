using System.Text;
using HamsterWheel.FluentCodeGenerators.Tokens;
using static HamsterWheel.FluentCodeGenerators.Chunks.Structure.LineWithPrependAndSuffixChunk;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public class FileScopeNamespaceChunk(INamespace @namespace) : ICodeChunk
{
    private readonly ICodeChunk? _namespaceChunk = WithSemicolon(@namespace.ToString(), prefix: "namespace ");

    public INamespace Namespace => @namespace;

    public static FileScopeNamespaceChunk From(INamespace @namespace) => new(@namespace);

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        _namespaceChunk?.AppendChunks(stringBuilder);
        return true;
    }
}