using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers;

public static class AssemblySymbolVisitor
{
    extension(IAssemblySymbol assemblySymbol)
    {
        public IEnumerable<INamespaceSymbol> GetAllNamespacesBy(Func<INamespaceSymbol, bool>? predicate = null)
        {
            var enumerable = assemblySymbol.GlobalNamespace.IterateSymbolsAndItsMembers().OfType<INamespaceSymbol>();
            return predicate is not null ? enumerable.Where(predicate) : enumerable;
        }

        public IEnumerable<INamedTypeSymbol> GetAllTypesBy(Func<INamedTypeSymbol, bool> predicate) =>
            assemblySymbol.GlobalNamespace.IterateSymbolsAndItsMembers()
            .OfType<INamedTypeSymbol>().Where(predicate);
    }

    public static IEnumerable<ISymbol> IterateSymbolsAndItsMembers(this INamespaceOrTypeSymbol ns)
    {
        yield return ns;
        foreach (var member in ns.GetMembers())
        {
            switch (member)
            {
                case INamedTypeSymbol type:
                    yield return type;
                    break;
                case INamespaceSymbol nestedNs:
                    {
                        foreach (var nested in nestedNs.IterateSymbolsAndItsMembers())
                        {
                            yield return nested;
                        }

                        break;
                    }
            }
        }
    }
}