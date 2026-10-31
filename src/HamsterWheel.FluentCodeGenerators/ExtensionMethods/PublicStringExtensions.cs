using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public static class PublicStringExtensions
{
    extension(string? str)
    {
        public Namespace ToNamespace() => Namespace.From(str);
        public PascalCaseName ToPascalCaseName() => str is not null ? new PascalCaseName(str) : new PascalCaseName("");
        public CamelCaseName ToCamelCaseName() => str is not null ? new CamelCaseName(str) : new CamelCaseName("");
        public ScreamingSnakeCaseName ToScreamingSnakeCaseName() => str is not null ? new ScreamingSnakeCaseName(str) : new ScreamingSnakeCaseName("");
    }
}