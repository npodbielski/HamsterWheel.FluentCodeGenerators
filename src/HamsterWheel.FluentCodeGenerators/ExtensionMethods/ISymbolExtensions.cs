using HamsterWheel.FluentCodeGenerators.External;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class ISymbolExtensions
{
    public static INamedTypeSymbol? GetTypeArgument(this ITypeSymbol symbol, int index) =>
        symbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol &&
        namedTypeSymbol.TypeArguments.Length > index
            ? namedTypeSymbol.TypeArguments[index] as INamedTypeSymbol
            : null;

    public static ClrTypeInfo? TranslateSymbolToClrType(this ITypeSymbol symbol)
    {
        var symbolName = symbol.ToString();
        var isNullable = false;
        if (symbolName.EndsWith("?"))
        {
            isNullable = true;
            symbolName = symbolName[..^1];
        }

        var clrType = Type.GetType($"{symbolName}, {symbol.ContainingAssembly}");
        if (clrType is not null)
        {
            return clrType;
        }

        var translated = symbol.Name switch
        {
            nameof(Object) => typeof(object),
            nameof(String) => typeof(string),
            nameof(Boolean) => typeof(bool),
            nameof(Int32) => typeof(int),
            nameof(Int16) => typeof(short),
            nameof(Int64) => typeof(long),
            nameof(Double) => typeof(double),
            nameof(Decimal) => typeof(decimal),
            nameof(Byte) => typeof(byte),
            nameof(Char) => typeof(char),
            nameof(DateTime) => typeof(DateTime),
            nameof(DateTimeOffset) => typeof(DateTimeOffset),
            nameof(Single) => typeof(float),
            nameof(SByte) => typeof(sbyte),
            nameof(UInt16) => typeof(ushort),
            nameof(UInt32) => typeof(uint),
            nameof(UInt64) => typeof(ulong),
            _ => null
        };

        return translated is not null ? new ClrTypeInfo(translated, isNullable) : null;
    }

    public static ExternalTypeInfo TranslateToExternalTypeInfo(this ITypeSymbol symbol)
    {
        if (symbol.TypeKind == TypeKind.Enum)
        {
            return new EnumTypeInfo(symbol.Name, symbol.ContainingNamespace.ToString().ToNamespace(),
                symbol.GetEnumNames());
        }

        return ExternalTypeInfo.From(symbol);
    }

    public static string[] GetEnumNames(this ITypeSymbol symbol) =>
        symbol.TypeKind == TypeKind.Enum
            ? symbol.GetMembers().Where(m => m.Kind == SymbolKind.Field).Select(s => s.Name).ToArray()
            : [];

    public static ISymbol? GetMemberFromInheritanceTree(this INamedTypeSymbol nameTypeSymbol, string memberName) =>
        GetMembersFromInheritanceTree(nameTypeSymbol, m => m.Name == memberName).FirstOrDefault();

    public static List<ISymbol> GetMembersFromInheritanceTree(this INamedTypeSymbol nameTypeSymbol,
        Func<ISymbol, bool> memberPredicate)
    {
        List<ISymbol> matches = [];
        var currentType = nameTypeSymbol;
        while (currentType is not null)
        {
            var members = currentType.GetMembers();
            var matchedMembers = members.Where(memberPredicate).ToArray();
            if (matchedMembers.Length > 0)
            {
                matches.AddRange(matchedMembers);
            }

            currentType = currentType.BaseType;
        }

        return matches;
    }

    public static (ClrTypeInfo?, ExternalTypeInfo?) ToClrOrExternalType(this ITypeSymbol? symbol)
    {
        if (symbol is null)
        {
            return (null, null);
        }

        var clrType = symbol.TranslateSymbolToClrType();
        if (clrType is not null)
        {
            return (clrType, null);
        }

        return (null, symbol.TranslateToExternalTypeInfo());
    }
}