using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

public static class CompilationSelectors
{
    public static string GetAssemblyName(Compilation compilation, string fallbackValue, CancellationToken _) =>
        compilation.AssemblyName ?? fallbackValue;

    public static Func<string, INamedTypeSymbol?> TypeResolver(Compilation compilation, CancellationToken _) =>
        compilation.GetTypeByMetadataName;
}