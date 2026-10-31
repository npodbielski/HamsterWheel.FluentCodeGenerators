using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

using AssemblyOrModuleResolverDelegate = Func<MetadataReference, ISymbol?>;

public static class ReferencesTransformations
{
    public static IEnumerable<IAssemblySymbol> ReferenceToSymbol(
        (AssemblyOrModuleResolverDelegate ToSymbol, IEnumerable<MetadataReference> References) c,
        CancellationToken _) =>
        c.References.Select(r => c.ToSymbol(r)).OfType<IAssemblySymbol>();
}