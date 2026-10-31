namespace HamsterWheel.FluentCodeGenerators.Tokens;

public record CamelCaseName(string NameAsString) : IName
{
    public CamelCaseName RemoveSuffix(string suffix) =>
        NameAsString.EndsWith(suffix)
            ? new CamelCaseName(NameAsString[..^suffix.Length])
            : new CamelCaseName(NameAsString);

    public CamelCaseName Append(string suffix) => new(NameAsString + suffix.ToPascalCase());

    public override string ToString() => NameAsString;

    public static implicit operator string(CamelCaseName name) => name.ToString();

    public static implicit operator CamelCaseName(string name) => new(name.ToCamelCase());
}