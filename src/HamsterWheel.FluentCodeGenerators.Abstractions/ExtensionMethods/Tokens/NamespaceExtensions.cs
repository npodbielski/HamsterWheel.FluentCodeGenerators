namespace HamsterWheel.FluentCodeGenerators.Tokens;

public static class NamespaceExtensions
{
    public static bool IsSystemNamespace(this INamespace @namespace) => @namespace.ToString().StartsWith("System");
    public static bool NotFrameworkPart(this INamespace @namespace) => !@namespace.IsSystemNamespace();
}