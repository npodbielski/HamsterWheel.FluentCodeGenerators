using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public interface ITypeResolverService
{
    INamedTypeSymbol GetExternalType(string name, Namespace namespaceName);
    INamedTypeSymbol GetExternalType(string fullName, int numberOfGenericParameters = 0);
    INamedTypeSymbol? GetExternalTypeOrNull(string fullName, int numberOfGenericParameters = 0);
}