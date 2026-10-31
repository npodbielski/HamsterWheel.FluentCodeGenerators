using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public static class TypeExtensions
{
    extension(Type type)
    {
        public bool IsGenericOf(Type expectedGeneric)
        {
            if (!expectedGeneric.IsGenericType)
            {
                throw new ArgumentException($"{nameof(expectedGeneric)} should be generic type!");
            }

            return type.IsGenericType && type.GetGenericTypeDefinition() == expectedGeneric.GetGenericTypeDefinition();
        }

        public bool IsNullable() => type.IsGenericOf(typeof(Nullable<>));

        public bool IsAttribute() => type.IsTypeOrSubclassOf(typeof(Attribute));

        public bool IsTypeOrSubclassOf(Type toCheck) =>
            type == toCheck || type.IsSubclassOf(toCheck);

        public NameInNamespace ToNameInNamespace()
        {
            if (!type.IsArray)
            {
                return NameInNamespace.From(type.Name, type.Namespace ?? "", false);
            }

            var elementType = type.GetElementType()!;
            return NameInNamespace.From(elementType.Name, elementType.Namespace ?? "", true);
        }
    }
}