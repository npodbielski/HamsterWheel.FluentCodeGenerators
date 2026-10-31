using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class MemberConditionUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _classChunk;

    public MemberConditionUnitTests()
    {
        _classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var context = new SourceCodeFileContext();
        _sut = new(context, _classChunk);
    }

    [Fact]
    public void Method_When_WithTruePredicate_ThenMemberIsRendered()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => m.When(_ => true));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Method_When_WithFalsePredicate_ThenMemberIsSuppressed()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithMethod(m => m.When(_ => false));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Method_When_WithNoPredicate_ThenMemberIsRendered()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => { });

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Method_When_PredicateReceivesCorrectName()
    {
        //arrange
        var predicateCapturedName = (string?)null;

        //act
        _sut.WithMethod(m => m.Named("MyMethod").When(md =>
        {
            predicateCapturedName = md.Name;
            return true;
        }));

        //assert
        ((object)predicateCapturedName).Should().Be("MyMethod");
    }

    [Fact]
    public void Method_When_PredicateReceivesCorrectParameters()
    {
        //arrange
        var predicateParameters = new List<string>();

        //act
        _sut.WithMethod(m => m
            .Named("MyMethod")
            .WithParameter(p => p.Named("param1").OfType<string>())
            .WithParameter(p => p.Named("param2").OfType<int>())
            .When(md =>
            {
                predicateParameters.AddRange(md.Parameters.Select(p => p.Name));
                return true;
            }));

        //assert
        ((object)predicateParameters).Should().BeEquivalentTo(new[] { "param1", "param2" });
    }

    [Fact]
    public void Method_When_PredicateReceivesCorrectDeclaringType()
    {
        //arrange
        var predicateDeclaringType = (string?)null;
        var predicateDeclaringNamespace = (string?)null;

        //act
        _sut.Named("MyClass")
            .WithMethod(m => m.When(md =>
            {
                predicateDeclaringType = md.DeclaringType.Name.ToString();
                predicateDeclaringNamespace = md.DeclaringType.Namespace.ToString();
                return true;
            }));

        //assert
        ((object)predicateDeclaringType).Should().Be("MyClass");
        ((object)predicateDeclaringNamespace).Should().Be("");
    }

    [Fact]
    public void Method_When_CanFilterByParameterCount()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void OnlyMethod()
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => m
            .Named("OnlyMethod")
            .When(md => md.Parameters.Length == 0));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Method_When_CanFilterByParameterType()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void StringMethod(string param)
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => m
            .Named("StringMethod")
            .WithParameter(p => p.Named("param").OfType<string>())
            .When(_ => true));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Method_WithParameter_WhenCalledWithArrayType_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void StringMethod(string[] param)
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => m
            .Named("StringMethod")
            .WithParameter(p => p.Named("param").OfType<string[]>())
            .When(_ => true));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Method_WithParameter_WhenCalledWithInt32ArrayType_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public void Int32Method(int[] param)
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => m
            .Named("Int32Method")
            .WithParameter(p => p.Named("param").OfType<int[]>())
            .When(_ => true));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Property_When_WithTruePredicate_ThenMemberIsRendered()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public string MyProperty { get; set; }
                                }
                                """;

        //act
        _sut.WithProp(p => p
            .Named("MyProperty")
            .OfType<string>()
            .When(_ => true));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Property_When_WithFalsePredicate_ThenMemberIsSuppressed()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithProp(p => p
            .Named("MyProperty")
            .OfType<string>()
            .When(_ => false));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Property_When_PredicateReceivesCorrectName()
    {
        //arrange
        var predicateCapturedName = (string?)null;

        //act
        _sut.WithProp(p => p
            .Named("MyProperty")
            .OfType<string>()
            .When(md =>
            {
                predicateCapturedName = md.Name;
                return true;
            }));

        //assert
        ((object)predicateCapturedName).Should().Be("MyProperty");
    }

    [Fact]
    public void Field_When_WithTruePredicate_ThenMemberIsRendered()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private object _myField;
                                }
                                """;

        //act
        _sut.WithField(f => f
            .Named("myField")
            .OfType<object>()
            .When(_ => true));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Field_When_WithFalsePredicate_ThenMemberIsSuppressed()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithField(f => f
            .Named("myField")
            .OfType<object>()
            .When(_ => false));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Field_When_PredicateReceivesCorrectName()
    {
        //arrange
        var predicateCapturedName = (string?)null;

        //act
        _sut.WithField(f => f
            .Named("myField")
            .OfType<object>()
            .When(md =>
            {
                predicateCapturedName = md.Name;
                return true;
            }));

        //assert
        ((object)predicateCapturedName).Should().Be("_myField");
    }

    [Fact]
    public void MultipleMembers_When_OnlyKeptMembersAreRendered()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private string _myField;

                                    public string KeptProperty { get; set; }

                                    public void KeptMethod()
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithMethod(m => m
            .Named("KeptMethod")
            .When(_ => true));

        _sut.WithMethod(m => m
            .Named("DiscardedMethod")
            .When(_ => false));

        _sut.WithProp(p => p
            .Named("KeptProperty")
            .OfType<string>()
            .When(_ => true));

        _sut.WithProp(p => p
            .Named("DiscardedProperty")
            .OfType<string>()
            .When(_ => false));

        _sut.WithField(f => f
            .Named("myField")
            .OfType<string>()
            .When(_ => true));

        _sut.WithField(f => f
            .Named("discardedField")
            .OfType<string>()
            .When(_ => false));

        //assert
        _classChunk.Should().RenderAs(expected);
    }
}