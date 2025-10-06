using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class FileScopedNamespaceContextExtensionsUnitTests
{
    private string Version => this.GetThisObjectTypeAssemblyVersion();

    private readonly IFileScopedNamespaceContext _sut = new SourceCodeFileContext().WithFileScopedNamespace("test");

    [Fact]
    public void WithClass_WhenNameAsStringPassed_ThenGeneratesCorrectFile()
    {
        //arrange
        var expected = $$"""
                         using System.CodeDom.Compiler;

                         namespace test;

                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                         public class MyClass
                         {

                         }
                         """;

        //act
        var actual = _sut.WithClass("MyClass");

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }

    [Fact]
    public void WithClass_WhenNameAsINamePassed_ThenGeneratesCorrectFile()
    {
        //arrange
        var expected = $$"""
                         using System.CodeDom.Compiler;

                         namespace test;

                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                         public class MyClass
                         {

                         }
                         """;

        //act
        var actual = _sut.WithClass("MyClass");

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }

    [Fact]
    public void WithSealedClass_WhenNameINamePassed_ThenGeneratesCorrectFile()
    {
        //arrange
        var expected = $$"""
                         using System.CodeDom.Compiler;

                         namespace test;

                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                         public sealed class MyClass
                         {

                         }
                         """;

        //act
        var actual = _sut.WithSealedClass("MyClass");

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }

    [Fact]
    public void WithSealedClass_WhenNameAsStringPassed_ThenGeneratesCorrectFile()
    {
        //arrange
        var expected = $$"""
                         using System.CodeDom.Compiler;

                         namespace test;

                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                         public sealed class MyClass
                         {

                         }
                         """;

        //act
        var actual = _sut.WithSealedClass("MyClass");

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }
}