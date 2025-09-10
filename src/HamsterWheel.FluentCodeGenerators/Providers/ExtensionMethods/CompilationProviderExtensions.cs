using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers;

using TypeResolverDelegate = Func<string, INamedTypeSymbol?>;

public static class CompilationProviderExtensions
{
    public static IncrementalValueProvider<string> GetAssemblyName(
        this IncrementalValueProvider<Compilation> compilation, string fallbackValue) =>
        compilation.Select((c, t) => CompilationSelectors.GetAssemblyName(c, fallbackValue, t));

    public static IncrementalValueProvider<TypeResolverDelegate> GetTypeResolver(
        this IncrementalValueProvider<Compilation> compilation) =>
        compilation.Select(CompilationSelectors.TypeResolver);
}