using HamsterWheel.FluentCodeGenerators.External;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

using TypeResolverDelegate = Func<string, INamedTypeSymbol?>;
using TypeTokenResolverDelegate = Func<string, TypeToken?>;
using AssemblyOrModuleResolverDelegate = Func<MetadataReference, ISymbol?>;

public static class CompilationSelectors
{
    public static string GetAssemblyName(Compilation compilation, string fallbackValue, CancellationToken _) =>
        compilation.AssemblyName ?? fallbackValue;

    [Obsolete("Use TypeTokenResolver; it hands out TypeToken, which carries the loaded type.")]
    public static TypeResolverDelegate TypeResolver(Compilation compilation, CancellationToken _) =>
        compilation.GetTypeByMetadataName;

    public static TypeTokenResolverDelegate TypeTokenResolver(Compilation compilation, CancellationToken _) =>
        name => compilation.GetTypeByMetadataName(name) is { } symbol
            ? TypeToken.From(symbol)
            : TypeToken.FromClr(name);

    public static AssemblyOrModuleResolverDelegate GetAssemblyOrModuleSymbolResolver(Compilation compilation, CancellationToken _) =>
        compilation.GetAssemblyOrModuleSymbol;

    public static ITypeResolverService TypeResolverService(Compilation compilation, CancellationToken _) =>
        new TypeResolverService(compilation.GetTypeByMetadataName);
}