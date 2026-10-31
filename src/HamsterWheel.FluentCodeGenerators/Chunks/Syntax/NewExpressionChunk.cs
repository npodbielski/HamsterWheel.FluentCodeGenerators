using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class NewExpressionChunk() : BodyChunk, ICodeWithNamespaceChunk
{
    private readonly string? _identifier;
    private readonly INamespace? _namespace;

    public NewExpressionChunk(string? identifier = null) : this() => _identifier = identifier;

    public NewExpressionChunk(INameInNamespaceToken? identifier = null) : this()
    {
        _identifier = identifier?.Name.NameAsString;
        _namespace = identifier?.Namespace;
    }

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        new NewKeywordChunk().AppendChunks(stringBuilder);
        if (_identifier is not null)
        {
            stringBuilder.AppendSingleSpace();
            new PlainValueChunk(_identifier).AppendChunks(stringBuilder);
        }

        new BracketsChunk(BracesType.Round).AppendChunks(stringBuilder);
        return true;
    }

    public INamespace[] Namespace => _namespace != null ? [_namespace] : [];
}