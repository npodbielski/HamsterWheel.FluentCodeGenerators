using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ClassContextUnitTests
{
    private readonly ClassContext _sut;
    private readonly ClassDefinitionChunk _classChunk;

    public ClassContextUnitTests()
    {
        _classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var context = new SourceCodeFileContext();
        _sut = new(context, _classChunk);
    }

    [Fact]
    public void NameAsString_WhenCalled_ThenReturnsNameOfClass()
    {
        //act && assert
        _sut.NameAsString.Should().Be("MyClass");
    }

    [Fact]
    public void Named_WhenRendered_ThenClassHaveCorrectName()
    {
        //arrange
        const string expected = """
                                public class MySuperbClass
                                {

                                }
                                """;

        //act
        _sut.Named("MySuperbClass");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithComment_WhenRendered_ThenClassHaveComment()
    {
        //arrange
        const string expected = """
                                /// <summary>
                                /// This is a comment
                                /// </summary>
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithComment("This is a comment");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCommentAndAttributes_WhenRendered_ThenHaveNewLinesBetweenCommentAndAttributes()
    {
        //arrange
        var version = this.GetThisObjectTypeAssemblyVersion();
        var expected = $$"""
                         /// <summary>
                         /// This is a comment
                         /// </summary>
                         [GeneratedCode("HamsterWheel.FluentCodeGenerators", "Version={{version}}")]
                         public class MyClass
                         {

                         }
                         """;
        _sut.WithGeneratedCodeAttr();

        //act
        _sut.WithComment("This is a comment");

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakePartial_WhenRendered_ThenClassIsPartial()
    {
        //arrange
        const string expected = """
                                public partial class MyClass
                                {

                                }
                                """;

        //act
        _sut.MakePartial();

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeStatic_WhenRendered_ThenClassIsStatic()
    {
        //arrange
        const string expected = """
                                public static class MyClass
                                {

                                }
                                """;

        //act
        _sut.MakeStatic();

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeAbstract_WhenRendered_ThenClassIsAbstract()
    {
        //arrange
        const string expected = """
                                public abstract class MyClass
                                {

                                }
                                """;

        //act
        _sut.MakeAbstract();

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeSealed_WhenRendered_ThenClassIsSealed()
    {
        //arrange
        const string expected = """
                                public sealed class MyClass
                                {

                                }
                                """;

        //act
        _sut.MakeSealed();

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithPrimaryCtor_WhenRendered_ThenHaveCtor()
    {
        //arrange
        const string expected = """
                                public class MyClass()
                                {

                                }
                                """;

        //act
        _sut.WithPrimaryCtor(_ => { });

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenRendered_ThenHaveBaseClass()
    {
        //arrange
        const string expected = """
                                public class MyClass : Exception
                                {

                                }
                                """;

        //act
        _sut.WithBase(b => b.From<Exception>());

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBase_WhenExtendedWithCtor_ThenCanReferencePrimaryCtorParameters()
    {
        //arrange
        _sut.Named("MyException");
        _sut.WithPrimaryCtor(pc => pc.WithParameter<string>("message"));
        const string expected = """
                                public class MyException(string message) : Exception(message)
                                {

                                }
                                """;

        //act
        _sut.WithBase(b =>
            b.From<Exception>()
                .WithCtorCall(bcc => bcc.WithParameter(p => p.UseExpression(b.ParametersNames[0]))));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCtor_WhenRendered_ThenHaveCtor()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public MyClass()
                                    {
                                    }
                                }
                                """;

        //act
        _sut.WithCtor(c => { });

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithMethod_WhenRendered_ThenHaveMethod()
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
        _sut.WithMethod(c => { });

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void ImplementsInterface_WhenRendered_ThenInheritsFromInterface()
    {
        //arrange
        const string expected = """
                                public class MyClass : IQueryable
                                {

                                }
                                """;

        //act
        _sut.ImplementsInterface(i => i.From<IQueryable>());

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithAttribute_WhenRendered_ThenHaveAttributeApplied()
    {
        //arrange
        const string expected = """
                                [ExcludeFromCodeCoverage]
                                public class MyClass
                                {

                                }
                                """;

        //act
        _sut.WithAttribute(i => i.From<ExcludeFromCodeCoverageAttribute>());

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCode_WhenRendered_ThenHaveCustomCode()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    custom code
                                }
                                """;

        //act
        _sut.WithCode(new StringBuilder("custom code"));

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithProp_WhenRendered_ThenHaveProp()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    public string Property { get; set; }
                                }
                                """;

        //act
        _sut.WithProp(p => { });

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithField_WhenRendered_ThenHaveField()
    {
        //arrange
        const string expected = """
                                public class MyClass
                                {
                                    private object _field1;
                                }
                                """;

        //act
        _sut.WithField(f => { });

        //assert
        _classChunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData(MemberVisibility.Internal, "internal")]
    [InlineData(MemberVisibility.Private, "private")]
    [InlineData(MemberVisibility.PrivateProtected, "protected private")]
    [InlineData(MemberVisibility.Protected, "protected")]
    [InlineData(MemberVisibility.ProtectedInternal, "protected internal")]
    [InlineData(MemberVisibility.Public, "public")]
    public void SetVisibility_WhenRendered_ThenHaveCorrectVisibilityModifier(MemberVisibility visibility,
        string expectedVisibility)
    {
        //arrange
        var expected = $$"""
                         {{expectedVisibility}} class MyClass
                         {

                         }
                         """;

        //act
        _sut.SetVisibility(visibility);

        //assert
        _classChunk.Should().RenderAs(expected);
    }
}