using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public static class INameInNamespaceTokenExtensions
{
    extension(INameInNamespaceToken namespaceToken)
    {
        public string GetFullName() =>
            string.Join(".", namespaceToken.Namespace.ToString(), namespaceToken.Name.ToString());

        public NameInNamespace Append(PascalCaseName suffix, Namespace? newNamespace = null) =>
            new(namespaceToken.Name.Append(suffix.ToString().ToPascalCase()), newNamespace ?? namespaceToken.Namespace);
    }
}