using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.External;

public record EnumTypeInfo(PascalCaseName Name, Namespace Namespace, string[] EnumValues) : ExternalTypeInfo(Name, Namespace)
{
    public static EnumTypeInfo FromParent(INameInNamespaceToken parent, PascalCaseName name, string[] values)
    {
        var nameInNamespace = parent.Append(name);
        return new EnumTypeInfo(nameInNamespace.Name.ToPascalCase(), nameInNamespace.Namespace, values);
    }
}