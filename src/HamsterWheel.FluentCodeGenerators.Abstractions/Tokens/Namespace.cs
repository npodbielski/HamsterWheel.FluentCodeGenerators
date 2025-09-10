namespace HamsterWheel.FluentCodeGenerators.Tokens;

public record Namespace(string? NamespaceAsString) : INamespace
{
    public static Namespace From(string? str) => new(str);

    public Namespace Append(string nestedNamespace) =>
        NamespaceAsString is not null ? new Namespace($"{NamespaceAsString}.{nestedNamespace.ToPascalCase()}") : this;

    public override string ToString() => NamespaceAsString ?? "";

    public static implicit operator string(Namespace name) => name.ToString();

    public static implicit operator Namespace(string? name) => new(name?.ToPascalCase());
}