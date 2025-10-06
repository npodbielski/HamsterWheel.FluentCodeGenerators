using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public static class INameInNamespaceTokenExtensions
{
    public static string GetFullName(this INameInNamespaceToken namespaceToken) =>
        string.Join(".", namespaceToken.Namespace.ToString(), namespaceToken.Name.ToString());

    public static NameInNamespace Append(this INameInNamespaceToken token, PascalCaseName suffix, Namespace? newNamespace = null) =>
        new(token.Name.Append(suffix.ToString().ToPascalCase()), newNamespace ?? token.Namespace);
}