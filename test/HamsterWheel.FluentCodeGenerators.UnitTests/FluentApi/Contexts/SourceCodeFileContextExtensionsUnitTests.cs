using System.Reflection;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class SourceCodeFileContextExtensionsUnitTests
{
    [Fact]
    public void NewWithNullabilityAndFileScopeNamespace_WhenCalled_ThenCreatesCorrectContext()
    {
        //arrange
        //act
        var actual = new SourceCodeFileContext().EnableNullability();

        //assert
        actual.BuildFile().Trim().Should().Be("#nullable enable");
    }

    [Fact]
    public void WithFileScopedNamespace_WhenCalled_ThenCreatesCorrectContext()
    {
        //arrange
        //act
        var actual = new SourceCodeFileContext().WithFileScopedNamespace("test");

        //assert
        actual.BuildFile().Trim().Should().Be("namespace test;");
    }

    [Fact]
    public void ConfigureUsing_WhenCalled_ThenRunsConfigurator()
    {
        //arrange
        //act
        var actual = new SourceCodeFileContext().ConfigureUsing<DummyConfigurator>();

        //assert
        actual.BuildFile().Trim().Should().Be("using test;");
    }

    [Fact]
    public void WithAssemblyAttribute_WhenCalledWithGenericAttribute_ThenAppliesAttribute()
    {
        //arrange
        //act
        var actual = new SourceCodeFileContext().WithAssemblyAttribute<AssemblyKeyNameAttribute>();

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System.Reflection;

                                              [assembly: AssemblyKeyName]
                                              """);
    }

    [Fact]
    public void WithAssemblyAttribute_WhenCalledWithGenericAttributeAndConfigure_ThenAppliesAttributeAndConfigures()
    {
        //arrange
        //act
        var actual = new SourceCodeFileContext().WithAssemblyAttribute<AssemblyKeyNameAttribute>(
            c => c.WithParameter(p => p.MakeNamed("keyName").UseStringValue("testKey")));

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                               using System.Reflection;

                                               [assembly: AssemblyKeyName(keyName = "testKey")]
                                               """);
    }

    [Fact]
    public void WithAssemblyAttribute_WhenCalledWithNameInNamespaceToken_ThenAppliesAttribute()
    {
        //arrange
        var typeToken = NameInNamespace.From("AssemblyKeyName", "System.Reflection");

        //act
        var actual = new SourceCodeFileContext().WithAssemblyAttribute(typeToken);

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System.Reflection;

                                              [assembly: AssemblyKeyName]
                                              """);
    }

    [Fact]
    public void WithAssemblyAttribute_WhenCalledWithNameInNamespaceTokenAndConfigure_ThenAppliesAttributeAndConfigures()
    {
        //arrange
        var typeToken = NameInNamespace.From("AssemblyKeyName", "System.Reflection");

        //act
        var actual = new SourceCodeFileContext().WithAssemblyAttribute(typeToken,
            c => c.WithParameter(p => p.MakeNamed("keyName").UseStringValue("anotherKey")));

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                               using System.Reflection;

                                               [assembly: AssemblyKeyName(keyName = "anotherKey")]
                                               """);
    }
}

public class DummyConfigurator : IContextConfigurator<ISourceCodeFileContext>
{
    public void Configure(ISourceCodeFileContext context) => context.WithUsings("test");
}