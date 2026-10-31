namespace HamsterWheel.FluentCodeGenerators.Tokens;

public record ScreamingSnakeCaseName(string NameAsString) : IName
{
    public override string ToString() => NameAsString;

    public static implicit operator string(ScreamingSnakeCaseName name) => name.ToString();

    public static implicit operator ScreamingSnakeCaseName(string name) => new(name.ToScreamingSnakeCase());
}
