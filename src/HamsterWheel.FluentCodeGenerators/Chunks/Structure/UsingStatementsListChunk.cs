using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class UsingStatementsListChunk(List<UsingStatementChunk>? usingChunks = null)
    : NewLineDelimitedJoinChunk(codeChunks: usingChunks)
{
    private List<UsingStatementChunk>? _usingChunks = usingChunks;

    public void Append(params INamespace[] namespaces)
    {
        foreach (var namespaceName in namespaces)
        {
            _usingChunks ??= [];
            if (_usingChunks.All(c => c.Namespace.ToString() != namespaceName.ToString()))
            {
                _usingChunks.Add(UsingStatementChunk.From(namespaceName));
            }
        }
    }

    public void AppendStatic(INameInNamespaceToken nameInNamespace)
    {
        _usingChunks ??= [];
        _usingChunks.Add(UsingStatementChunk.StaticFrom(nameInNamespace));
    }

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (_usingChunks is null)
        {
            return false;
        }

        var frameworkNamespaces =
            _usingChunks.Where(n => n.Namespace.IsSystemNamespace()).OrderBy(n => n.Namespace.ToString());
        var userOrLibsNamespaces =
            _usingChunks.Where(n => n.Namespace.NotFrameworkPart()).OrderBy(n => n.Namespace.ToString());
        var joiner = new NewLineDelimitedJoinChunk(codeChunks: [..frameworkNamespaces, ..userOrLibsNamespaces]);
        joiner.AppendChunks(stringBuilder);
        stringBuilder.AppendLine();

        return true;
    }
}