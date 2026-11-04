using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.External;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.External;

public class TypeTokenUnitTests
{

    [Fact]
    public void FromClr_WhenCalledWithKnownType_ThenRuntimeAndIdentityAreFilled()
    {
        //act
        var actual = TypeToken.FromClr(typeof(int));

        //assert
        actual.Name.NameAsString.Should().Be(nameof(Int32));
        actual.Namespace.NamespaceAsString.Should().Be(typeof(int).Namespace);
        actual.Runtime.Should().Be(typeof(int));
        actual.IsNullable.Should().BeFalse();
    }

    [Fact]
    public void FromClr_WhenCalledWithNullable_ThenIsNullableIsSetAndRuntimeIsTheOriginal()
    {
        //act
        var actual = TypeToken.FromClr(typeof(int?));

        //assert
        actual.IsNullable.Should().BeTrue();
        actual.Name.NameAsString.Should().Be(nameof(Int32));
        actual.Runtime.Should().Be(typeof(int?));
    }

    [Fact]
    public void FromClr_WhenCalledWithEnum_ThenMemberNamesAreCarried()
    {
        //act
        var actual = TypeToken.FromClr(typeof(Operation));

        //assert
        actual.IsEnum.Should().BeTrue();
        actual.EnumValues.Should().Equal("Add", "Remove");
    }

    [Fact]
    public void FromClr_WhenCalledWithConstructedGeneric_ThenArgumentsAreTokens()
    {
        //act
        var actual = TypeToken.FromClr(typeof(Dictionary<string, int>));

        //assert
        actual.GenericArgs.Length.Should().Be(2);
        actual.GenericArgs[0].Runtime.Should().Be(typeof(string));
        actual.GenericArgs[1].Name.NameAsString.Should().Be(nameof(Int32));
        actual.Arity.Should().Be(2);
    }

    [Fact]
    public void FromClr_WhenCalledWithOpenGeneric_ThenArityNamesTheParametersToClose()
    {
        //act
        var actual = TypeToken.FromClr(typeof(IDictionary<,>));

        //assert
        actual.Arity.Should().Be(2);
        actual.GenericArgs.Length.Should().Be(0);
        actual.NumberOfGenericArgs.Should().Be(2);
    }

    [Fact]
    public void FromClr_WhenCalledByName_ThenOnlyLoadableTypesComeBack()
    {
        //act / assert
        TypeToken.FromClr("System.Int32")!.Runtime.Should().Be(typeof(int));
        TypeToken.FromClr("No.Such.Type").Should().BeNull();
    }

    [Fact]
    public void From_WhenCalledWithSymbolOfKnownType_ThenRuntimeIsResolvedFromTheSymbol()
    {
        //arrange
        var symbol = GetCompilation().GetTypeByMetadataName("System.Int32")!;

        //act
        var actual = TypeToken.From(symbol);

        //assert
        actual.Name.NameAsString.Should().Be(nameof(Int32));
        actual.Runtime.Should().Be(typeof(int));
    }

    [Fact]
    public void From_WhenCalledWithSymbolOfUnknownType_ThenTextsRemainAndRuntimeStaysNull()
    {
        //arrange
        var symbol = GetCompilation("namespace Test.Space; public enum Operation { Add, Remove }")
            .GetTypeByMetadataName("Test.Space.Operation")!;

        //act
        var actual = TypeToken.From(symbol);

        //assert
        actual.Name.NameAsString.Should().Be("Operation");
        actual.Namespace.NamespaceAsString.Should().Be("Test.Space");
        actual.IsEnum.Should().BeTrue();
        actual.EnumValues.Should().Equal("Add", "Remove");
        actual.Runtime.Should().BeNull();
    }

    [Fact]
    public void From_WhenCalledWithNullableValueTypeSymbol_ThenIsNullableIsSet()
    {
        //arrange
        var holder = GetCompilation(
            "namespace Test.Space; public class Holder { public int? Count { get; set; } }")
            .GetTypeByMetadataName("Test.Space.Holder")!;
        var symbol = ((IPropertySymbol)holder.GetMembers("Count").First()).Type;

        //act
        var actual = TypeToken.From(symbol);

        //assert
        actual.IsNullable.Should().BeTrue();
        actual.Name.NameAsString.Should().Be(nameof(Int32));
    }

    [Fact]
    public void TokensOfTheSameTypeFromBothSources_ThenTheyAreEqual()
    {
        //arrange
        var fromClr = TypeToken.FromClr(typeof(int));
        var fromSymbol = TypeToken.From(GetCompilation().GetTypeByMetadataName("System.Int32")!);

        //act / assert: runtime knowledge is not identity
        fromSymbol.Equals(fromClr).Should().BeTrue();
    }

    [Fact]
    public void ExplicitCastFromType_WhenDone_ThenItIsTheClrToken()
    {
        //act
        var actual = (TypeToken)typeof(int);

        //assert
        actual.Runtime.Should().Be(typeof(int));
    }

    private static Compilation GetCompilation(string? source = null)
    {
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => (MetadataReference)MetadataReference.CreateFromFile(a.Location))
            .ToList();
        var trees = new List<SyntaxTree>();
        if (source is not null)
        {
            trees.Add(CSharpSyntaxTree.ParseText(source));
        }

        return CSharpCompilation.Create("TestAssembly", trees, references);
    }

    private enum Operation
    {
        Add,
        Remove,
    }
}
