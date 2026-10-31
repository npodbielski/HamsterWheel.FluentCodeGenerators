using System.Reflection;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Factories;

public class TypedConstantFactory
{
    private readonly ConstructorInfo _typedConstantCtor;

    public TypedConstantFactory()
    {
        _typedConstantCtor = typeof(TypedConstant)
            .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .First(c => c.GetParameters().Length == 3);
    }

    public TypedConstant CreateWithType(INamedTypeSymbol namedTypeSymbol) =>
        (TypedConstant)_typedConstantCtor.Invoke((object[])
        [
            null!,
            TypedConstantKind.Primitive,
            namedTypeSymbol
        ]);

    public TypedConstant CreateWithEnum<TEnum>(TEnum @enum) where TEnum : Enum =>
        (TypedConstant)_typedConstantCtor.Invoke((object[])
        [
            null!,
            TypedConstantKind.Enum,
            (int)(object)@enum
        ]);
}