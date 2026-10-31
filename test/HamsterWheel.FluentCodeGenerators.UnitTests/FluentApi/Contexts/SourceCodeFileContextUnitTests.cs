using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Dummies;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class SourceCodeFileContextUnitTests
{
    private readonly SourceCodeFileContext _sut = new();

    [Fact]
    public void EnableNullability_WhenCalled_ThenEmitsPragma()
    {
        //arrange
        const string expected = "#nullable enable";

        //act
        var actual = _sut.EnableNullability();

        //assert
        actual.BuildFile().Trim().Should().Be(expected);
    }

    [Fact]
    public void EnableNullability_WhenCalled_ThenNullabilityIsEnabled()
    {
        //arrange
        //act
        var actual = _sut.EnableNullability();

        //assert
        actual.Should().BeAssignableTo<INullabilitySettings>()
            .Subject.Enabled.Should().BeTrue();
    }

    [Fact]
    public void WithUsingsT1_WhenCalled_ThenAddUsing()
    {
        //arrange
        //act
        var actual = _sut.WithUsings<int>();

        //assert
        actual.BuildFile().Trim().Should().Be("using System;");
    }

    [Fact]
    public void WithUsingsT1_WhenCalledWithDuplicatedNamespace_ThenDoNotAddUsing()
    {
        //arrange
        _sut.WithUsings<int>();

        //act
        var actual = _sut.WithUsings<DateTime>();

        //assert
        actual.BuildFile().Trim().Should().Be("using System;");
    }

    [Fact]
    public void WithUsingsT2_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings<DateTime, MatchType>();

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System;
                                              using System.IO;
                                              """);
    }

    [Fact]
    public void WithUsingsT3_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings<DateTime, MatchType, IPAddress>();

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System;
                                              using System.IO;
                                              using System.Net;
                                              """);
    }

    [Fact]
    public void WithUsingsT4_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings<DateTime, MatchType, double, FactAttribute>();

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System;
                                              using System.IO;
                                              using Xunit;
                                              """);
    }

    [Fact]
    public void WithUsingsT5_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings<DateTime, MatchType, double, FactAttribute, TypeUsageContext>();

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System;
                                              using System.IO;
                                              using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
                                              using Xunit;
                                              """);
    }

    [Fact]
    public void WithUsingsArrayOfType_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings(typeof(IDbCommand), typeof(EntityHandle));

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System.Data;
                                              using System.Reflection.Metadata;
                                              """);
    }

    [Fact]
    public void WithUsingsArrayOfStrings_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings("System", "HamsterWheel.FluentCodeGenerators.UnitTests");

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System;
                                              using HamsterWheel.FluentCodeGenerators.UnitTests;
                                              """);
    }

    [Fact]
    public void WithUsingsArrayOfINamespace_WhenCalled_ThenAddUsings()
    {
        //arrange
        //act
        var actual = _sut.WithUsings("System", "HamsterWheel.FluentCodeGenerators.UnitTests");

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System;
                                              using HamsterWheel.FluentCodeGenerators.UnitTests;
                                              """);
    }

    [Fact]
    public void AddStaticUsing_WhenCalledWithNameInNamespace_ThenAddsStaticUsing()
    {
        //arrange
        //act
        ((IUsingsAppender)_sut).AddStaticUsing(new NameInNamespace("DataMapper", "HamsterWheel.Common.Data.Mapping"));

        //assert
        _sut.BuildFile().Trim().Should().Be("using static HamsterWheel.Common.Data.Mapping.DataMapper;");
    }

    [Fact]
    public void AddUsing_WhenCalledWithITypeSymbol_ThenAddsUsing()
    {
        //arrange
        //act
        _sut.AddUsing(new DummyNamedTypeSymbol("ExternalName", "ExternalNamespace"));

        //assert
        _sut.BuildFile().Trim().Should().Be("using ExternalNamespace;");
    }

    [Fact]
    public void AddPragma_WhenCalledWithNullability_ThenAddsPragma()
    {
        //arrange
        //act
        var actual = _sut.AddPragma(c => c.Nullability(c => c.Disable()));

        //assert
        actual.BuildFile().Trim().Should().Be("#nullable disable");
    }

    [Fact]
    public void WithAssemblyAttribute_WhenCalledWitAttributeType_ThenAddsAttribute()
    {
        //arrange
        //act
        var actual = _sut.WithAssemblyAttribute(c => c.From<InternalsVisibleToAttribute>());

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              using System.Runtime.CompilerServices;

                                              [assembly: InternalsVisibleTo]
                                              """);
    }

    [Fact]
    public void WithFileScopedNamespace_WhenCalledWithNullability_ThenAddsPragma()
    {
        //arrange
        //act
        var actual = _sut.WithFileScopedNamespace("test");

        //assert
        actual.BuildFile().Trim().Should().Be("namespace test;");
    }

    [Fact]
    public void NewWithNullability_WhenCalled_ThenCreatesCorrectContext()
    {
        //arrange
        //act
        var actual = SourceCodeFileContext.NewWithNullability();

        //assert
        actual.BuildFile().Trim().Should().Be("#nullable enable");
    }

    [Fact]
    public void NewWithNullabilityAndFileScopeNamespace_WhenCalled_ThenCreatesCorrectContext()
    {
        //arrange
        //act
        var actual = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("test".ToNamespace());

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              #nullable enable

                                              namespace test;
                                              """);
    }

    [Fact]
    public void AddExternAlias_WhenCalledWithAlias_ThenAddsAlias()
    {
        //arrange
        //act
        var actual = _sut.AddExternAlias("a");

        //assert
        actual.BuildFile().Trim().Should().Be("extern alias a;");
    }

    [Fact]
    public void AddExternAlias_WhenCalledWith2Aliases_ThenAddsBoth()
    {
        //arrange
        //act
        var actual = _sut.AddExternAlias("a").AddExternAlias("b");

        //assert
        actual.BuildFile().Trim().Should().Be("""
                                              extern alias a;
                                              extern alias b;
                                              """);
    }
}