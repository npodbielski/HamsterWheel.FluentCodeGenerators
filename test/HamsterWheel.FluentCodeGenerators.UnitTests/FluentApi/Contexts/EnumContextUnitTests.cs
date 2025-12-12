using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Enums;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class EnumContextUnitTests
{
    private readonly EnumChunk _chunk;
    private readonly EnumContext _sut;

    public EnumContextUnitTests()
    {
        _chunk = new EnumChunk();
        var context = new SourceCodeFileContext();
        _sut = EnumContext.From(context, _chunk);
    }

    [Fact]
    public void Named_WhenRendered_ThenClassHaveCorrectName()
    {
        //arrange
        const string expected = """
                                public enum NewEnum
                                {
                                }
                                """;

        //act
        _sut.Named("NewEnum");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Named_WhenCalled_ThenNameAsStringHaveCorrectValue()
    {
        //arrange
        const string expected = "NewEnum";

        //act
        _sut.Named(expected);

        //assert
        _sut.NameAsString.Should().Be(expected);
    }

    [Fact]
    public void WithValues_WhenRendered_ThenEnumHaveValues()
    {
        //arrange
        const string expected = """
                                public enum MyEnum
                                {
                                    One,
                                    Two,
                                }
                                """;

        //act
        _sut.WithValues(["one", "two"]);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData("one", "One")]
    [InlineData("Two", "Two")]
    [InlineData("TwoWords", "TwoWords")]
    [InlineData("twoWords", "TwoWords")]
    public void WithValue_WhenRendered_ThenEnumHaveValues(string providedValue, string expectedValue)
    {
        //arrange
        var expected = $$"""
                         public enum MyEnum
                         {
                             {{expectedValue}},
                         }
                         """;

        //act
        _sut.WithValue(providedValue);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithValueWithNumber_WhenRendered_ThenEnumHaveNumberAfterName()
    {
        //arrange
        var expected = """
                       public enum MyEnum
                       {
                           MyValue = 1000,
                       }
                       """;

        //act
        _sut.WithValue("MyValue", 1000);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithValueWithSnakeCase_WhenRendered_ThenEnumHaveAttribute()
    {
        //arrange
        var expected = """
                       public enum MyEnum
                       {
                           [EnumMember(Value = "Minus-Delimited-Value")]
                           MinusDelimitedValue,
                       }
                       """;

        //act
        _sut.WithValue("Minus-Delimited-Value");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData(MemberVisibility.Internal, "internal")]
    [InlineData(MemberVisibility.Private, "private")]
    [InlineData(MemberVisibility.PrivateProtected, "protected private")]
    [InlineData(MemberVisibility.Protected, "protected")]
    [InlineData(MemberVisibility.ProtectedInternal, "protected internal")]
    [InlineData(MemberVisibility.Public, "public")]
    public void SetVisibility_WhenRendered_ThenHaveField(MemberVisibility visibility, string expectedVisibility)
    {
        //arrange
        var expected = $$"""
                         {{expectedVisibility}} enum MyEnum
                         {
                         }
                         """;

        //act
        _sut.SetVisibility(visibility);

        //assert
        _chunk.Should().RenderAs(expected);
    }
}