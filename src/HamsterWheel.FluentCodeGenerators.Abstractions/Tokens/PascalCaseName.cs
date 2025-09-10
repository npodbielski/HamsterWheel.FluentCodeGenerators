using System.Collections.Generic;
using System.Linq;

namespace HamsterWheel.FluentCodeGenerators.Tokens;

public record PascalCaseName(string NameAsString) : IName
{
    public PascalCaseName RemoveSuffix(string suffix) =>
        NameAsString.EndsWith(suffix)
            ? new PascalCaseName(NameAsString[..^suffix.Length])
            : new PascalCaseName(NameAsString);

    public PascalCaseName Append(string suffix) => new(NameAsString + suffix.ToPascalCase());

    public override string ToString() => NameAsString;

    public static implicit operator string(PascalCaseName name) => name.ToString();

    public static implicit operator PascalCaseName(string name) => new(name.ToPascalCase());

    public static PascalCaseName FromChunks(params IEnumerable<PascalCaseName> enumerable)
    {
        var name = enumerable.Aggregate(new PascalCaseName(""), (current, chunk) => current.Append(chunk));
        return new PascalCaseName(name.ToString());
    }
}