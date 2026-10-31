using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class TypeUsageContextUnitTests
{
    private readonly TypeNameChunk _chunk;
    private readonly TypeUsageContext _sut;

    public TypeUsageContextUnitTests()
    {
        _chunk = TypeNameChunk.From<int>();
        var context = new SourceCodeFileContext();
        _sut = new TypeUsageContext(context, _chunk);
    }

    [Fact]
    public void From_WhenCalled_ThenRendersCorrectType()
    {
        //arrange
        var expected = "int";

        //act
        _sut.From(nameof(Int32).ToPascalCaseName(), "System".ToNamespace());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalled_ThenAddsUsing()
    {
        //arrange
        //act
        _sut.From(nameof(Int32).ToPascalCaseName(), "System".ToNamespace());

        //assert
        _sut.Usings.Should().RenderAs("using System;");
    }

    [Fact]
    public void From_WhenCalledWithNullable_ThenRendersTypeWithQuestionMark()
    {
        //arrange
        //act
        _sut.From<Nullable<int>>();

        //assert
        _chunk.Should().RenderAs("int?");
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenRendersCorrectType()
    {
        //arrange
        var expected = "IQueryable<int>";

        //act
        _sut.From(typeof(IQueryable<>).Name.ToPascalCaseName(), "System.Linq".ToNamespace());
        _sut.WithGenericArgument(t => t.From("int".ToPascalCaseName()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithGenericArgument_WhenCalled_ThenAddsUsings()
    {
        //arrange
        //act
        _sut.From(typeof(IQueryable<>).Name.ToPascalCaseName(), "System.Linq".ToNamespace())
            .WithGenericArgument(t => t.From(typeof(IClassContext).Name.ToPascalCaseName(),
                "HamsterWheel.FluentCodeGenerators.Fluent.Contexts".ToNamespace()));

        //assert
        _sut.Usings.Should().RenderAs("""
                                      using System.Linq;
                                      using HamsterWheel.FluentCodeGenerators.Fluent.Contexts;
                                      """);
    }

    [Fact]
    public void MakeArray_WhenCalled_ThenRendersCorrectType()
    {
        //arrange
        //act
        _sut.From(typeof(int).Name.ToPascalCaseName()).MakeArray();

        //assert
        _chunk.Should().RenderAs("int[]");
    }

    [Fact]
    public void MakeNullable_WhenCalled_ThenRendersCorrectType()
    {
        //arrange
        //act
        _sut.From(typeof(int).Name.ToPascalCaseName()).MakeNullable();

        //assert
        _chunk.Should().RenderAs("int?");
    }

    [Fact]
    public void MakeNullable_WhenCalledOnTask_ThenRendersArgumentNullableNotTask()
    {
        //arrange
        //act
        _sut.From<Task<int>>().MakeNullable();

        //assert
        _chunk.Should().RenderAs("Task<int?>");
    }

    [Fact]
    public void WithGenericArgument_WhenOnClosedGeneric_ThenThrows()
    {
        //arrange
        var context = _sut.From(typeof(Task<>)).WithGenericArgument<int>();
        var action = () => context.WithGenericArgument<decimal>();

        //act
        var actual = action.Should().Throw<InvalidNumberOfGenericArgumentsException>();

        //assert
        actual.WithMessage("Invalid number of generic arguments provided: *");
    }

    [Theory]
    [InlineData("SByte", "sbyte")]
    [InlineData("Byte", "byte")]
    [InlineData("Int16", "short")]
    [InlineData("UInt16", "ushort")]
    [InlineData("Int32", "int")]
    [InlineData("UInt32", "uint")]
    [InlineData("Int64", "long")]
    [InlineData("IntPtr", "nint")]
    [InlineData("UIntPtr", "nuint")]
    [InlineData("Decimal", "decimal")]
    [InlineData("String", "string")]
    [InlineData("String[]", "string[]")]
    [InlineData("Object", "object")]
    [InlineData("Void", "void")]
    public void MapTypeToKeyword_WhenCalledWithType_ThenMapsItToKeyword(string typeName, string expected)
    {
        //arrange
        //act
        var actual = TypeNameChunk.MapTypeToKeyword(typeName);

        //assert
        actual.Should().Be(expected);
    }
}