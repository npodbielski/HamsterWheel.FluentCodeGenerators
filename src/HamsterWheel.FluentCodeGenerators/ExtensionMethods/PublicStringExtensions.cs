using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public static class PublicStringExtensions
{
    public static Namespace ToNamespace(this string? str) => Namespace.From(str);

    public static PascalCaseName ToPascalCaseName(this string? str) => str is not null ? new PascalCaseName(str) : new PascalCaseName("");
    public static CamelCaseName ToCamelCaseName(this string? str) => str is not null ? new CamelCaseName(str) : new CamelCaseName("");
}