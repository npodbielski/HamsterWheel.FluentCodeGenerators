using System.Reflection;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

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
    public void WithAssemblyAttribute_WhenCalled_ThenAppliesAttribute()
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
}

public class DummyConfigurator : IContextConfigurator<ISourceCodeFileContext>
{
    public void Configure(ISourceCodeFileContext context) => context.WithUsings("test");
}