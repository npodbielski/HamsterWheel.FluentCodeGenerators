using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;

#pragma warning disable RS1009
public class DummyNamedTypeSymbol(string name, string nameSpace) : INamedTypeSymbol
#pragma warning restore RS1009
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
    public string Name { get; } = name;
    public string MetadataName { get; }
    public int MetadataToken { get; }
    public ISymbol ContainingSymbol { get; }
    public IAssemblySymbol ContainingAssembly { get; }
    public IModuleSymbol ContainingModule { get; }
    public INamedTypeSymbol ContainingType { get; }
    public INamespaceSymbol ContainingNamespace { get; } = new DummyNamespaceSymbol(nameSpace);
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
    public INamedTypeSymbol OriginalDefinition { get; }
    public IMethodSymbol? DelegateInvokeMethod { get; }
    public INamedTypeSymbol? EnumUnderlyingType { get; }
    public INamedTypeSymbol ConstructedFrom { get; }
    public ImmutableArray<IMethodSymbol> InstanceConstructors { get; }
    public ImmutableArray<IMethodSymbol> StaticConstructors { get; }
    public ImmutableArray<IMethodSymbol> Constructors { get; }
    public ISymbol? AssociatedSymbol { get; }
    public bool MightContainExtensionMethods { get; }
    public INamedTypeSymbol? TupleUnderlyingType { get; }
    public ImmutableArray<IFieldSymbol> TupleElements { get; }
    public bool IsSerializable { get; }
    public INamedTypeSymbol? NativeIntegerUnderlyingType { get; }

    public ImmutableArray<CustomModifier> GetTypeArgumentCustomModifiers(int ordinal)
    {
        throw new NotImplementedException();
    }

    public INamedTypeSymbol Construct(params ITypeSymbol[] typeArguments)
    {
        throw new NotImplementedException();
    }

    public INamedTypeSymbol Construct(ImmutableArray<ITypeSymbol> typeArguments,
        ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    public INamedTypeSymbol ConstructUnboundGenericType()
    {
        throw new NotImplementedException();
    }

    public int Arity { get; }
    public bool IsGenericType { get; }
    public bool IsUnboundGenericType { get; }
    public bool IsScriptClass { get; }
    public bool IsImplicitClass { get; }
    public bool IsComImport { get; }
    public bool IsFileLocal { get; }
    public IEnumerable<string> MemberNames { get; }
    public ImmutableArray<ITypeParameterSymbol> TypeParameters { get; }
    public ImmutableArray<ITypeSymbol> TypeArguments { get; }
    public ImmutableArray<NullableAnnotation> TypeArgumentNullableAnnotations { get; }

    ITypeSymbol ITypeSymbol.OriginalDefinition => OriginalDefinition;

    public SpecialType SpecialType { get; }
    public bool IsRefLikeType { get; }
    public bool IsUnmanagedType { get; }
    public bool IsReadOnly { get; }
    public bool IsRecord { get; }
    public NullableAnnotation NullableAnnotation { get; }

    public ISymbol? FindImplementationForInterfaceMember(ISymbol interfaceMember)
    {
        throw new NotImplementedException();
    }

    public string ToDisplayString(NullableFlowState topLevelNullability, SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToDisplayParts(NullableFlowState topLevelNullability,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public string ToMinimalDisplayString(SemanticModel semanticModel, NullableFlowState topLevelNullability,
        int position,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel,
        NullableFlowState topLevelNullability, int position,
        SymbolDisplayFormat? format = null)
    {
        throw new NotImplementedException();
    }

    public ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
    {
        throw new NotImplementedException();
    }

    public TypeKind TypeKind { get; }
    public INamedTypeSymbol? BaseType { get; }
    public ImmutableArray<INamedTypeSymbol> Interfaces { get; }
    public ImmutableArray<INamedTypeSymbol> AllInterfaces { get; }
    public bool IsReferenceType { get; }
    public bool IsValueType { get; }
    public bool IsAnonymousType { get; }
    public bool IsTupleType { get; }
    public bool IsNativeIntegerType { get; }

    ISymbol ISymbol.OriginalDefinition => OriginalDefinition;

    public bool HasUnsupportedMetadata { get; }

    public ImmutableArray<ISymbol> GetMembers()
    {
        throw new NotImplementedException();
    }

    public ImmutableArray<ISymbol> GetMembers(string name)
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