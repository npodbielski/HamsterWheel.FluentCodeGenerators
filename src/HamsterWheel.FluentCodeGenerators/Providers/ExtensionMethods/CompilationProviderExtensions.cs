using HamsterWheel.FluentCodeGenerators.Providers.Transformations;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers;

using TypeResolverDelegate = Func<string, INamedTypeSymbol?>;
using AssemblyOrModuleResolverDelegate = Func<MetadataReference, ISymbol?>;
using static ReferencesTransformations;

public static class CompilationProviderExtensions
{
    extension(IncrementalValueProvider<Compilation> compilation)
    {
        public IncrementalValueProvider<string> GetAssemblyName(string fallbackValue) =>
            compilation.Select((c, t) => CompilationSelectors.GetAssemblyName(c, fallbackValue, t));

        public IncrementalValueProvider<TypeResolverDelegate> GetTypeResolver() =>
            compilation.Select(CompilationSelectors.TypeResolver);

        public IncrementalValueProvider<ITypeResolverService> GetTypeResolverService() =>
            compilation.Select(CompilationSelectors.TypeResolverService);

        public IncrementalValueProvider<AssemblyOrModuleResolverDelegate> GetAssemblyOrModuleResolver() =>
            compilation.Select(CompilationSelectors.GetAssemblyOrModuleSymbolResolver);

        public IncrementalValueProvider<IEnumerable<IAssemblySymbol>> GetReferencesAsSymbolsBy(
            Func<IEnumerable<MetadataReference>, IEnumerable<MetadataReference>>? selector = null)
        {
            var referencesToSymbol = compilation.Select(CompilationSelectors.GetAssemblyOrModuleSymbolResolver);
            var selectedReferences =
                compilation.Select((c, _) => selector is not null ? selector.Invoke(c.References) : c.References);
            return referencesToSymbol.Combine(selectedReferences).Select(ReferenceToSymbol);
        }
    }
}