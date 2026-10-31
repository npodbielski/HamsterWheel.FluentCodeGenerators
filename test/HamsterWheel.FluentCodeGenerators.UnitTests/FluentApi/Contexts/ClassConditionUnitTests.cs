using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;

// Note: IFileScopedNamespaceContext is used via the return type of NewWithNullabilityAndFileScopeNamespace

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ClassConditionUnitTests
{
    [Fact]
    public void When_WithTruePredicate_ThenClassIsRendered()
    {
        //arrange
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("MyClass").When(_ => true));

        //assert
        var output = fileContext.BuildFile();
        output.Should().Contain("#nullable enable");
        output.Should().Contain("namespace Namespace;");
        output.Should().Contain("public class MyClass");
    }

    [Fact]
    public void When_WithFalsePredicate_ThenClassIsSuppressed()
    {
        //arrange
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("MyClass").When(_ => false));

        //assert - class is suppressed, only namespace scope rendered
        var output = fileContext.BuildFile();
        output.Should().NotContain("public class MyClass");
    }

    [Fact]
    public void When_PredicateReceivesCorrectClassName()
    {
        //arrange
        string? predicateCapturedName = null;
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("MyClass").When(def =>
        {
            predicateCapturedName = def.Name;
            return true;
        }));

        //assert
        predicateCapturedName.Should().Be("MyClass");
    }

    [Fact]
    public void When_PredicateReceivesDeclaringType_WhenNamespaceSet()
    {
        //arrange
        NameInNamespace? predicateCapturedDeclaringType = null;
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("MyClass").When(def =>
        {
            predicateCapturedDeclaringType = def.DeclaringType;
            return true;
        }));

        //assert - DeclaringType holds the class name and namespace from the file context
        predicateCapturedDeclaringType.Should().NotBeNull();
        predicateCapturedDeclaringType.Name.NameAsString.Should().Be("MyClass");
    }

    [Fact]
    public void When_WithMethod_WhenClassPredicateTrue_ThenMemberIsRendered()
    {
        //arrange
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("MyClass")
            .WithMethod(m => m.Named("MyMethod"))
            .When(_ => true));

        //assert
        var output = fileContext.BuildFile();
        output.Should().Contain("public class MyClass");
        output.Should().Contain("public void MyMethod");
    }

    [Fact]
    public void When_WithMethod_WhenClassPredicateFalse_ThenClassAndMembersAreSuppressed()
    {
        //arrange
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("MyClass")
            .WithMethod(m => m.Named("MyMethod"))
            .When(_ => false));

        //assert - class and method are suppressed
        var output = fileContext.BuildFile();
        output.Should().NotContain("public class MyClass");
        output.Should().NotContain("public void MyMethod");
    }

    [Fact]
    public void When_WithoutPredicate_ThenClassIsRendered()
    {
        //arrange
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("Test"));

        //assert
        var output = fileContext.BuildFile();
        output.Should().Contain("public class Test");
    }

    [Fact]
    public void When_CanFilterByClassName()
    {
        //arrange
        string? predicateCapturedName = null;
        var fileContext = SourceCodeFileContext.NewWithNullabilityAndFileScopeNamespace("Namespace".ToNamespace());

        //act
        fileContext.WithClass(c => c.Named("PublicApi").When(def =>
        {
            predicateCapturedName = def.Name;
            return def.Name == "PublicApi";
        }));

        //assert
        predicateCapturedName.Should().Be("PublicApi");
    }
}