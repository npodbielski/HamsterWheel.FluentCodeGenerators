using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public static class TokenTypeExtensions
{
    public static NameInNamespace ToNameInNamespace(this Type type) =>
        NameInNamespace.From(type.Name, type.Namespace ?? "");
}