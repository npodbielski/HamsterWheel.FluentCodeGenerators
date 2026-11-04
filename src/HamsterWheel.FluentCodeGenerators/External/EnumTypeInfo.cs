using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.External;

[Obsolete("Use TypeToken; it carries the member names in EnumValues.")]
public record EnumTypeInfo(
    PascalCaseName Name,
    Namespace Namespace,
    string[] EnumValues) : ExternalTypeInfo(Name, Namespace)
{
    public static EnumTypeInfo From(INamedTypeSymbol symbol) =>
        new(new PascalCaseName(symbol.Name), symbol.ContainingNamespace.ToString().ToNamespace(),
            symbol.GetEnumNames());
}