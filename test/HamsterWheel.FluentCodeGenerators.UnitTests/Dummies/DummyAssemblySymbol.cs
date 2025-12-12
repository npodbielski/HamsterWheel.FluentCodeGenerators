#pragma warning disable RS1009
using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;

public class DummyAssemblySymbol(string name) : IAssemblySymbol
{
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

    public bool GivesAccessTo(IAssemblySymbol toAssembly)
    {
        throw new NotImplementedException();
    }

    public INamedTypeSymbol? GetTypeByMetadataName(string fullyQualifiedMetadataName)
    {
        throw new NotImplementedException();
    }

    public INamedTypeSymbol? ResolveForwardedType(string fullyQualifiedMetadataName)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<INamedTypeSymbol> GetForwardedTypes()
    {
        throw new NotImplementedException();
    }

    public AssemblyMetadata? GetMetadata()
    {
        throw new NotImplementedException();
    }

    public bool IsInteractive { get; }
    public AssemblyIdentity Identity { get; }
    public INamespaceSymbol GlobalNamespace { get; }
    public IEnumerable<IModuleSymbol> Modules { get; }
    public ICollection<string> TypeNames { get; }
    public ICollection<string> NamespaceNames { get; }
    public bool MightContainExtensionMethods { get; }

    public override string ToString() => name;
}