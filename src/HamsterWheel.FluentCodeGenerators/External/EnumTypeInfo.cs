using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.External;

public record EnumTypeInfo(PascalCaseName Name, Namespace Namespace, string[] EnumValues) : ExternalTypeInfo(Name, Namespace)
{
    public static EnumTypeInfo From(INamedTypeSymbol symbol) =>
        new(symbol.Name, symbol.ContainingNamespace.ToString().ToNamespace(),
            symbol.GetEnumNames());
}