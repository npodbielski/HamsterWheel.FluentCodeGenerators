namespace HamsterWheel.FluentCodeGenerators.Tokens;

public static class INameExtensions
{
    public static string FirstLetterLower(this IName name) => name.ToString()[0].ToString().ToLower();
    public static PascalCaseName ToPascalCase(this IName name) => name.ToString();
    public static CamelCaseName ToCamelCase(this IName name) => name.ToString();

    private static bool IsInterface(this IName name) =>
        name.ToString().StartsWith("I") && char.IsUpper(name.ToString()[1]);

    public static CamelCaseName ToParameterName(this IName name) =>
        name.IsInterface() ? new CamelCaseName(name.ToString()[1..]) : name.ToCamelCase();

    public static CamelCaseName ToFieldName(this IName name)
    {
        var nameAsString = name.ToString().TrimStart('_');
        return new CamelCaseName("_" + (name.IsInterface() ? new CamelCaseName(nameAsString[1..]) : nameAsString.ToCamelCase()));
    }

    public static PascalCaseName ToPlural(this IName name) => new(name.ToString().ToPlural());

    public static PascalCaseName ToSingular(this IName name) => new(name.ToString().ToSingular());

    public static NameInNamespace InNamespace(this IName name, INamespace targetNamespace) =>
        new(name.ToPascalCase(), new Namespace(targetNamespace.NamespaceAsString));
}