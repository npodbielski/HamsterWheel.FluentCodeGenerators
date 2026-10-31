using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public class TypeResolverService(Func<string, INamedTypeSymbol?> resolver) : ITypeResolverService
{
    public INamedTypeSymbol GetExternalType(string name, Namespace namespaceName) =>
        GetExternalType(name.ToPascalCaseName().InNamespace(namespaceName).GetFullName());

    public INamedTypeSymbol GetExternalType(string fullName, int numberOfGenericParameters = 0) =>
        GetExternalTypeOrNull(fullName, numberOfGenericParameters) ??
        throw new NoExternalTypeFoundException(fullName);

    public INamedTypeSymbol? GetExternalTypeOrNull(string fullName, int numberOfGenericParameters = 0)
    {
        if (numberOfGenericParameters != 0)
        {
            fullName = $"{fullName}`{numberOfGenericParameters}";
        }

        return resolver(fullName);
    }
}