namespace HamsterWheel.FluentCodeGenerators.Tokens;

public static class NamespaceExtensions
{
    extension(INamespace @namespace)
    {
        public bool IsSystemNamespace() => @namespace.ToString().StartsWith("System");
        public bool NotFrameworkPart() => !@namespace.IsSystemNamespace();
    }
}