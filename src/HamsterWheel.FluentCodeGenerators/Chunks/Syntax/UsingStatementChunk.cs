using System.Text;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class UsingStatementChunk(Namespace namespaceName, IName? name = null) : ICodeChunk
{
    public INamespace Namespace => namespaceName;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        new UsingKeywordChunk().AppendChunks(stringBuilder);
        if (name is not null)
        {
            new StaticKeywordChunk().AppendChunks(stringBuilder);
        }

        stringBuilder.Append(namespaceName);
        if (name is not null)
        {
            stringBuilder.Append('.');
            stringBuilder.Append(name);
        }

        stringBuilder.Append(';');

        return true;
    }

    public static UsingStatementChunk From(INamespace namespaceName) =>
        new(new Namespace(namespaceName.NamespaceAsString));

    public static UsingStatementChunk StaticFrom(INameInNamespaceToken nameInNamespace) =>
        new(nameInNamespace.Namespace, nameInNamespace.Name);
}