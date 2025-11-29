namespace HamsterWheel.FluentCodeGenerators;

public static class TypeExtensions
{
    public static bool IsGenericOf(this Type type, Type expectedGeneric)
    {
        if (!expectedGeneric.IsGenericType)
        {
            throw new ArgumentException($"{nameof(expectedGeneric)} should be generic type!");
        }

        return type.IsGenericType && type.GetGenericTypeDefinition() == expectedGeneric.GetGenericTypeDefinition();
    }

    public static bool IsNullable(this Type type) => type.IsGenericOf(typeof(Nullable<>));

    public static bool IsTypeOrSubclassOf(this Type type, Type toCheck) =>
        type == toCheck || type.IsSubclassOf(toCheck);

    public static bool IsAttribute(this Type type) => type.IsTypeOrSubclassOf(typeof(Attribute));
}