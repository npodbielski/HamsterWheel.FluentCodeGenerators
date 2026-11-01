using HamsterWheel.FluentCodeGenerators.External;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class ISymbolExtensions
{
    extension(ISymbol symbol)
    {
        public INamedTypeSymbol? GetTypeArgument(int index = 0) =>
            symbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol &&
            namedTypeSymbol.TypeArguments.Length > index
                ? namedTypeSymbol.TypeArguments[index] as INamedTypeSymbol
                : null;

        public ClrTypeInfo? TranslateSymbolToClrType()
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
                return new ClrTypeInfo(clrType, IsNullable: isNullable);
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

        public AttributeData? GetAttributeOfType<T>() =>
            symbol.GetAttributes().FirstOrDefault(a => a.AttributeClass?.Name == typeof(T).Name);

        public bool HaveAttributeOfType<T>() =>
            symbol.GetAttributes().Any(a => a.AttributeClass?.Name == typeof(T).Name);

        public AttributeData? GetAttributeOfType(INamedTypeSymbol type) =>
            symbol.GetAttributes()
                .FirstOrDefault(a => a.AttributeClass?.Equals(type, SymbolEqualityComparer.Default) == true);

        public AttributeData? GetAttributeOfType(string type) =>
            symbol.GetAttributes().FirstOrDefault(a => a.AttributeClass?.Name == type);
    }

    extension(ITypeSymbol symbol)
    {
        /// <summary>The constructed interface the type implements, when it implements this one.
        /// The generic contract goes in, its construction on the type comes out, so its
        /// TypeArguments are readable.</summary>
        public INamedTypeSymbol? InterfaceImplementation(INamedTypeSymbol contract)
        {
            var definition = contract.OriginalDefinition;
            foreach (var implemented in symbol.AllInterfaces)
            {
                if (SymbolEqualityComparer.Default.Equals(implemented.OriginalDefinition, definition))
                {
                    return implemented;
                }
            }

            return null;
        }

        public bool ImplementsInterface(INamedTypeSymbol contract) =>
            symbol.InterfaceImplementation(contract) is not null;

        public ExternalTypeInfo TranslateToExternalTypeInfo() => symbol.TypeKind == TypeKind.Enum
            ? EnumTypeInfo.From((INamedTypeSymbol)symbol)
            : ExternalTypeInfo.From(symbol);

        public string[] GetEnumNames() =>
            symbol.TypeKind == TypeKind.Enum
                ? symbol.GetMembers().Where(m => m.Kind == SymbolKind.Field).Select(s => s.Name).ToArray()
                : [];
    }

    extension(ITypeSymbol? symbol)
    {
        public (ClrTypeInfo?, ExternalTypeInfo?) ToClrOrExternalType()
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
}