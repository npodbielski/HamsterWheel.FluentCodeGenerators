using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class FileScopedNamespaceContextUnitTests
{
    private string Version => this.GetThisObjectTypeAssemblyVersion();

    [Fact]
    public void WithClass_WhenCalled_ThenGeneratesClass()
    {
        //arrange
        var expected = $$"""
                         using System.CodeDom.Compiler;

                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                         public class NewClass
                         {

                         }
                         """;
        var sut = new FileScopedNamespaceContext(new SourceCodeFileContext());

        //act
        var actual = sut.WithClass(c => c.Named("NewClass"));

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }

    [Fact]
    public void WithEnum_WhenCalled_ThenGeneratesEnum()
    {
        //arrange
        var expected = $$"""
                         using System.CodeDom.Compiler;

                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{Version}}")]
                         public enum MyEnum
                         {
                         }
                         """;
        var sut = new FileScopedNamespaceContext(new SourceCodeFileContext());

        //act
        var actual = sut.WithEnum(c => c.Named("MyEnum"));

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }
}