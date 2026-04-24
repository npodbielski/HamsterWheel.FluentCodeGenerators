using System.Runtime.CompilerServices;
using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

[InterpolatedStringHandler]
#pragma warning disable CS9113 // Parameter is unread. REASON: this is necessary for interpolated strings. 
public readonly struct BodyBuilderInterpolatedStringHandler(int literalLength, int formattedCount)
#pragma warning restore CS9113 // Parameter is unread.
{
    // Storage for the built-up string
    private readonly StringBuilder _builder = new(literalLength);

    public void AppendLiteral(string s) => _builder.Append(s);

    public void AppendFormatted<T>(T t)
    {
        switch (t)
        {
            case INameInNamespaceToken typeToken:
                Namespaces.Add(typeToken.Namespace);
                _builder.Append(typeToken.Name);
                break;
            case ITypeSymbol typeSymbol:
                Namespaces.Add(typeSymbol.TranslateToExternalTypeInfo().Namespace);
                _builder.Append(typeSymbol.Name);
                break;
            case INamedChunk namedEntity:
                _builder.Append(namedEntity.Name);
                break;
            case ICodeChunk codeChunk:
                _builder.Append(codeChunk.Build());
                break;
            case Type type:
                Namespaces.Add(type.Namespace.ToNamespace());
                _builder.Append(type.Name);
                break;
            default:
                _builder.Append(t);
                break;
        }
    }

    internal string GetFormattedText() => _builder.ToString();
    internal List<INamespace> Namespaces { get; } = [];
}