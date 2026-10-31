using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

using TypeResolverDelegate = Func<string, INamedTypeSymbol?>;
using AssemblyOrModuleResolverDelegate = Func<MetadataReference, ISymbol?>;

public static class CompilationSelectors
{
    public static string GetAssemblyName(Compilation compilation, string fallbackValue, CancellationToken _) =>
        compilation.AssemblyName ?? fallbackValue;

    public static TypeResolverDelegate TypeResolver(Compilation compilation, CancellationToken _) =>
        compilation.GetTypeByMetadataName;

    public static AssemblyOrModuleResolverDelegate GetAssemblyOrModuleSymbolResolver(Compilation compilation, CancellationToken _) =>
        compilation.GetAssemblyOrModuleSymbol;

    public static ITypeResolverService TypeResolverService(Compilation compilation, CancellationToken _) =>
        new TypeResolverService(compilation.GetTypeByMetadataName);
}