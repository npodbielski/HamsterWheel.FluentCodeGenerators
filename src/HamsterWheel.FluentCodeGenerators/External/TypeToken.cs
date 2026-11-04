using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.External;

/// <summary>
/// Unified type info for generator
/// </summary>
public sealed record TypeToken(
    PascalCaseName Name,
    Namespace Namespace,
    bool IsNullable = false,
    EquatableArray<TypeToken> GenericArgs = default,
    EquatableArray<string> EnumValues = default,
    EquatableArray<string> Attributes = default,
    bool IsArray = false,
    int Arity = 0) : NameInNamespace(Name, Namespace, IsArray), IExternalTypeInfo
{
    /// <summary>
    /// Equivalent CLR type if exists.
    /// </summary>
    public Type? Runtime { get; init; }

    /// <summary>
    /// True when the token names an enum; EnumValues then carries its member names.
    /// </summary>
    public bool IsEnum => EnumValues.Length > 0;

    /// <summary>
    /// The generic parameters the name takes: an open definition declares them all, a
    /// constructed type carries the ones already given.
    /// </summary>
    public int NumberOfGenericArgs => Arity;

    public override string ToString() => Name.ToString();

    //identity members only - Runtime and Attributes are resolved knowledge, two equal tokens
    //may differ in them, so the hash must agree with Equals and leave them out too
    public bool Equals(TypeToken? other) =>
        other is not null
            && Name == other.Name
            && Namespace == other.Namespace
            && IsNullable == other.IsNullable
            && GenericArgs.Equals(other.GenericArgs)
            && EnumValues.Equals(other.EnumValues)
            && IsArray == other.IsArray
            && Arity == other.Arity;

    public override int GetHashCode() =>
        new[]
        {
            17,
            Name.GetHashCode(),
            Namespace.GetHashCode(),
            IsNullable.GetHashCode(),
            GenericArgs.GetHashCode(),
            EnumValues.GetHashCode(),
            IsArray.GetHashCode(),
            Arity,
        }.Aggregate((current, hash) => (current * 31) + hash);

    /// <summary>
    /// The token of a loaded type, read off its type name ("System.Int32").
    /// </summary>
    public static TypeToken FromClr(Type type)
    {
        var nullable = Nullable.GetUnderlyingType(type) is not null;
        var element = nullable ? Nullable.GetUnderlyingType(type)! : type;
        var isArray = element.IsArray;
        if (isArray)
        {
            element = element.GetElementType()!;
        }

        var args = element.GenericTypeArguments.Length > 0
            ? (EquatableArray<TypeToken>)Array.ConvertAll(element.GenericTypeArguments, FromClr)
            : default;
        var enumValues = element.IsEnum
            ? (EquatableArray<string>)Enum.GetNames(element)
            : default;
        EquatableArray<string> attributes =
            Array.ConvertAll(element.GetCustomAttributes(true), static a => a.GetType().Name);
        return new TypeToken(
            new PascalCaseName(element.Name),
            element.Namespace.ToNamespace(),
            nullable,
            args,
            enumValues,
            attributes,
            isArray,
            element.IsGenericType ? element.GetGenericArguments().Length : 0)
        {
            Runtime = type,
        };
    }

    /// <summary>
    /// The token of a loaded type named by its full name ("System.DateTime"); null when
    /// this process cannot load it.
    /// </summary>
    public static TypeToken? FromClr(string fullTypeName) =>
        Type.GetType(fullTypeName) is { } type ? FromClr(type) : null;

    /// <summary>
    /// The token of a type the compilation declares: texts always, and the loaded type
    /// too when this process can name the same type (BCL and referenced assemblies).
    /// </summary>
    public static TypeToken From(ISymbol symbol)
    {
        var typeSymbol = (ITypeSymbol)symbol;
        var isArray = typeSymbol.Kind == SymbolKind.ArrayType;
        if (isArray)
        {
            typeSymbol = ((IArrayTypeSymbol)typeSymbol).ElementType;
        }

        if (typeSymbol is INamedTypeSymbol
            {
                TypeArguments.Length: 1,
                OriginalDefinition.SpecialType: SpecialType.System_Nullable_T
            } nullableOf)
        {
            return From(nullableOf.TypeArguments[0]) with { IsNullable = true };
        }

        var named = typeSymbol as INamedTypeSymbol;
        var isNullable = typeSymbol.NullableAnnotation == NullableAnnotation.Annotated;
        var enumValues = typeSymbol.TypeKind == TypeKind.Enum
            ? (EquatableArray<string>)typeSymbol.GetEnumNames()
            : default;
        var attributes = (EquatableArray<string>)AttributesOf(symbol).ToArray();
        var args = named is { TypeArguments.Length: > 0 } constructed
            ? (EquatableArray<TypeToken>)constructed.TypeArguments.Select(From).ToArray()
            : default;
        var arity = named switch
        {
            { OriginalDefinition: { } definition } => Math.Max(
                definition.TypeArguments.Length, named.TypeArguments.Length),
            _ => 0,
        };
        var token = new TypeToken(
            new PascalCaseName(typeSymbol.Name.ToPascalCase()),
            typeSymbol.ContainingNamespace?.ToString().ToNamespace() ?? new Namespace(null),
            isNullable,
            args,
            enumValues,
            attributes,
            isArray,
            arity);
        return ResolveRuntime(symbol, token) is { } runtime
            ? token with { Runtime = runtime }
            : token;
    }

    private static IEnumerable<string> AttributesOf(ISymbol symbol)
    {
        try
        {
            return symbol.GetAttributes()
                .Select(a => a.AttributeClass?.Name)
                .Where(static n => n != null)!;
        }
        catch (NullReferenceException)
        {
            //a symbol stub without attribute data declares no attributes
            return [];
        }
    }

    //the loaded type behind a declared symbol: its own assembly first, then the BCL names the
    //language guarantees
    private static Type? ResolveRuntime(ISymbol symbol, TypeToken token)
    {
        var fullName = token.Namespace.NamespaceAsString is { Length: > 0 } ns
            ? $"{ns}.{token.Name.NameAsString}"
            : token.Name.NameAsString;
        if (Type.GetType($"{fullName}, {symbol.ContainingAssembly?.Name}") is { } loaded)
        {
            return loaded;
        }

        return BclTypes.TryGetValue(fullName, out var bcl) ? bcl : null;
    }

    private static readonly Dictionary<string, Type> BclTypes = new()
    {
        ["System.Object"] = typeof(object),
        ["System.String"] = typeof(string),
        ["System.Boolean"] = typeof(bool),
        ["System.Byte"] = typeof(byte),
        ["System.SByte"] = typeof(sbyte),
        ["System.Int16"] = typeof(short),
        ["System.UInt16"] = typeof(ushort),
        ["System.Int32"] = typeof(int),
        ["System.UInt32"] = typeof(uint),
        ["System.Int64"] = typeof(long),
        ["System.UInt64"] = typeof(ulong),
        ["System.Single"] = typeof(float),
        ["System.Double"] = typeof(double),
        ["System.Decimal"] = typeof(decimal),
        ["System.Char"] = typeof(char),
        ["System.DateTime"] = typeof(DateTime),
        ["System.DateTimeOffset"] = typeof(DateTimeOffset),
        ["System.Guid"] = typeof(Guid),
        ["System.TimeSpan"] = typeof(TimeSpan),
    };

    /// <summary>
    /// The token of a loaded type, read off its type name ("System.Int32").
    /// </summary>
    public static explicit operator TypeToken(Type type) => FromClr(type);
}
