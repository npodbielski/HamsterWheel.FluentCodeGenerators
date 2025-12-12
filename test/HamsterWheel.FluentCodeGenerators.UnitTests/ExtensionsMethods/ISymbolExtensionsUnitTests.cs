using System.Linq.Expressions;
using System.Net;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.External;
using HamsterWheel.FluentCodeGenerators.UnitTests.Dummies;
using Microsoft.CodeAnalysis;
using NSubstitute;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public class ISymbolExtensionsUnitTests
{
    [Fact]
    public void GetTypeArgument_WhenCalledWithNotINamedSymbol_ThenNull()
    {
        //arrange
        var symbol = new DummyTypeSymbol();

        //act
        var actual = symbol.GetTypeArgument(0);

        //assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetTypeArgument_WhenCalledWithINamedSymbolButNotGeneric_ThenNull()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("test", "test");

        //act
        var actual = symbol.GetTypeArgument(0);

        //assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetTypeArgument_WhenCalledWithINamedSymbolButLessGenericParameters_ThenNull()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("test", "test", isGenericType: true)
        {
            TypeArguments = [new DummyTypeSymbol(), new DummyTypeSymbol()]
        };

        //act
        var actual = symbol.GetTypeArgument(3);

        //assert
        actual.Should().BeNull();
    }


    [Fact]
    public void GetTypeArgument_WhenCalledWithINamedSymbolButParametersIsNotNamedTypeSymbol_ThenNull()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("test", "test", isGenericType: true)
        {
            TypeArguments = [new DummyTypeSymbol(), new DummyTypeSymbol()]
        };

        //act
        var actual = symbol.GetTypeArgument(0);

        //assert
        actual.Should().BeNull();
    }

    [Fact]
    public void GetTypeArgument_WhenCalledWithINamedSymbolThatIsGenericWithCorrectIndex_ThenReturnsArgument()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("test", "test", isGenericType: true)
        {
            TypeArguments = [new DummyNamedTypeSymbol("testarg", "namespace"), new DummyTypeSymbol()]
        };

        //act
        var actual = symbol.GetTypeArgument(0);

        //assert
        actual.Should().Be(symbol.TypeArguments[0]);
    }

    [Theory]
    [MemberData(nameof(SymbolsToTranslate))]
    public void TranslateSymbolToClrType_WhenCalledWithObject_ThenReturnsArgument(string name, string @namespace,
        ClrTypeInfo expected)
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol(name, @namespace);

        //act
        var actual = symbol.TranslateSymbolToClrType();

        //assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void TranslateSymbolToClrType_WhenCanLoadType_ThenReturnsClrTypeInfo()
    {
        //arrange
        var expected = typeof(IPAddress);
        var symbol = new DummyNamedTypeSymbol(expected.Name, expected.Namespace, false, expected.Assembly.FullName);

        //act
        var actual = symbol.TranslateSymbolToClrType();

        //assert
        actual.Should().Be(new ClrTypeInfo(expected));
    }

    [Fact]
    public void TranslateSymbolToClrType_WhenNullable_ThenReturnsClrTypeInfoWithIsNullable()
    {
        //arrange
        var expected = typeof(int);
        var symbol = new DummyNamedTypeSymbol("Int32?", "System", false, expected.Assembly.FullName);

        //act
        var actual = symbol.TranslateSymbolToClrType();

        //assert
        actual.Should().Be(new ClrTypeInfo(expected) { IsNullable = true });
    }

    [Fact]
    public void TranslateToExternalTypeInfo_WhenNotEnum_ThenReturnExternalTypeInfo()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("Object", "System");
        var expected = ExternalTypeInfo.From(symbol);

        //act
        var actual = symbol.TranslateToExternalTypeInfo();

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void TranslateToExternalTypeInfo_WhenEnum_ThenReturnExternalTypeInfo()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("SomeEnum", "HamsterWheel")
        {
            TypeKind = TypeKind.Enum,
            EnumMembers = [ "One", "Two" ]
        };
        var expected = EnumTypeInfo.From(symbol);

        //act
        var actual = symbol.TranslateToExternalTypeInfo();

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetEnumNames_WhenEnum_ThenReturnExternalTypeInfo()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("SomeEnum", "HamsterWheel")
        {
            TypeKind = TypeKind.Enum,
            EnumMembers = [ "One", "Two" ],
            OtherMembers = [new DummyNamedTypeSymbol("test", "test")]
        };
        string[] expected = [ "One", "Two" ];

        //act
        var actual = symbol.GetEnumNames();

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ToClrOrExternalType_WhenExternalType_ThenReturnExternalTypeInfo()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("SomeEnum", "HamsterWheel");

        //act
        var actual = symbol.ToClrOrExternalType();

        //assert
        actual.Should().Be((null, ExternalTypeInfo.From(symbol)));
    }

    [Fact]
    public void ToClrOrExternalType_WhenClr_ThenReturnClrTypeInfo()
    {
        //arrange
        var symbol = new DummyNamedTypeSymbol("Object", "System");

        //act
        var actual = symbol.ToClrOrExternalType();

        //assert
        actual.Should().Be((new ClrTypeInfo(typeof(object)), null));
    }

    [Fact]
    public void ToClrOrExternalType_WhenSymbolNull_ThenReturnsNull()
    {
        //arrange
        INamedTypeSymbol? symbol = null;

        //act
        var actual = symbol!.ToClrOrExternalType();

        //assert
        actual.Should().Be((null, null));
    }

    public static TheoryData<string, string, ClrTypeInfo> SymbolsToTranslate =>
        new()
        {
            { "Object", "System", new ClrTypeInfo(typeof(object)) },
            { "String", "System", new ClrTypeInfo(typeof(string)) },
            { "Boolean", "System", new ClrTypeInfo(typeof(bool)) },
            { "Int32", "System", new ClrTypeInfo(typeof(int)) },
            { "Int16", "System", new ClrTypeInfo(typeof(short)) },
            { "Int64", "System", new ClrTypeInfo(typeof(long)) },
            { "Double", "System", new ClrTypeInfo(typeof(double)) },
            { "Decimal", "System", new ClrTypeInfo(typeof(decimal)) },
            { "Byte", "System", new ClrTypeInfo(typeof(byte)) },
            { "Char", "System", new ClrTypeInfo(typeof(char)) },
            { "DateTime", "System", new ClrTypeInfo(typeof(DateTime)) },
            { "DateTimeOffset", "System", new ClrTypeInfo(typeof(DateTimeOffset)) },
            { "Single", "System", new ClrTypeInfo(typeof(float)) },
            { "SByte", "System", new ClrTypeInfo(typeof(sbyte)) },
            { "UInt16", "System", new ClrTypeInfo(typeof(ushort)) },
            { "UInt32", "System", new ClrTypeInfo(typeof(uint)) },
            { "UInt64", "System", new ClrTypeInfo(typeof(ulong)) },
            { null, "System", null },
        };
}