namespace HamsterWheel.FluentCodeGenerators.Tokens;

public record NameInNamespace(PascalCaseName Name, Namespace Namespace) : INameInNamespaceToken
{
    public override string ToString() => Name.ToString();

    public static NameInNamespace From(string name, string @namespace) => new(new PascalCaseName(name), @namespace);
}