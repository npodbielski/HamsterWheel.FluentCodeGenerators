using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.External;

public record ExternalTypeInfo(
    PascalCaseName Name,
    Namespace Namespace,
    int NumberOfGenericArgs = 0,
    string[]? Attributes = null,
    bool IsArray = false) : IExternalTypeInfo
{
    public static ExternalTypeInfo From<T>() => From(typeof(T));

    public static ExternalTypeInfo From(Type type) =>
        new(type.Name, type.Namespace.ToNamespace(), type.GenericTypeArguments.Length,
            type.GetCustomAttributes(true).Select(a => a.GetType().Name).ToArray());

    public static ExternalTypeInfo From(ITypeSymbol symbol)
    {
        string[] attributes =
            symbol.GetAttributes().Select(a => a.AttributeClass?.Name).Where(n => n != null).ToArray()!;
        var isArray = symbol.Kind == SymbolKind.ArrayType;
        symbol = isArray ? ((IArrayTypeSymbol)symbol).ElementType : symbol;

        var nameSpace = symbol.ContainingNamespace.ToString();
        return new ExternalTypeInfo(symbol.Name, nameSpace.ToNamespace(),
            (symbol as INamedTypeSymbol)?.TypeArguments.Length ?? 0, attributes, IsArray: isArray);
    }

    public override string ToString() => Name;
}