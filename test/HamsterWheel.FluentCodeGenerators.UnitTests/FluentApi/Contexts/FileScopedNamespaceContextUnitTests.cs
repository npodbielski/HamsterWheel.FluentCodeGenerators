using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class FileScopedNamespaceContextUnitTests
{
    private const string Version = "0.4.1.0";

    [Fact]
    public void WithClass_WhenCalled_ThenGeneratesClass()
    {
        //arrange
        const string expected = $$"""
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
        const string expected = $$"""
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