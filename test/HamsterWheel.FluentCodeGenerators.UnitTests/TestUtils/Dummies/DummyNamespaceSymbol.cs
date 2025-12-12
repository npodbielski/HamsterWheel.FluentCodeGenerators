using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

#pragma warning disable RS1009
public class DummyNamespaceSymbol(string nameSpace) : INamespaceSymbol
#pragma warning restore RS1009
{
    override public string ToString() => nameSpace;

    public bool Equals(ISymbol? other)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<AttributeData> GetAttributes()
    {
        throw new NotImplementedException();
    }

    public void Accept(SymbolVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public TResult? Accept<TResult>(SymbolVisitor<TResult> visitor)
    {
        throw new NotImplementedException();
    }

    public TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument)
    {
        throw new NotImplementedException();
    }

    public string? GetDocumentationCommentId()
    {
        throw new NotImplementedException();
    }

    public string? GetDocumentationCommentXml(CultureInfo? preferredCulture = null, bool expandIncludes = false,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public string ToDisplayString(SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToDisplayParts(SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public string ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, int position,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public bool Equals(ISymbol? other, SymbolEqualityComparer equalityComparer)
    {
        throw new NotImplementedException();
    }

    public SymbolKind Kind { get; }
    public string Language { get; }
    public string Name { get; }
    public string MetadataName { get; }
    public int MetadataToken { get; }
    public ISymbol ContainingSymbol { get; }
    public IAssemblySymbol ContainingAssembly { get; }
    public IModuleSymbol ContainingModule { get; }
    public INamedTypeSymbol ContainingType { get; }
    public INamespaceSymbol ContainingNamespace { get; }
    public bool IsDefinition { get; }
    public bool IsStatic { get; }
    public bool IsVirtual { get; }
    public bool IsOverride { get; }
    public bool IsAbstract { get; }
    public bool IsSealed { get; }
    public bool IsExtern { get; }
    public bool IsImplicitlyDeclared { get; }
    public bool CanBeReferencedByName { get; }
    public ImmutableArray<Location> Locations { get; }
    public ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get; }
    public Accessibility DeclaredAccessibility { get; }
    public ISymbol OriginalDefinition { get; }
    public bool HasUnsupportedMetadata { get; }

    ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers()
    {
        throw new NotImplementedException();
    }

    IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<INamespaceSymbol> GetNamespaceMembers()
    {
        throw new NotImplementedException();
    }

    public bool IsGlobalNamespace { get; }
    public NamespaceKind NamespaceKind { get; }
    public Compilation? ContainingCompilation { get; }
    public ImmutableArray<INamespaceSymbol> ConstituentNamespaces { get; }

    IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    public bool IsNamespace { get; }
    public bool IsType { get; }
}