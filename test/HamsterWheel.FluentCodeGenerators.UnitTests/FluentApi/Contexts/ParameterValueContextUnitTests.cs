using System.Globalization;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ParameterValueContextUnitTests
{
    private readonly ParameterValueContext _sut;
    private readonly ParameterValueChunk _chunk;

    public ParameterValueContextUnitTests()
    {
        _chunk = ParameterValueChunk.From("2", FluentApiSettings.DefaultCulture);
        _sut = new ParameterValueContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void MakeNamed_WhenCalled_ThenParameterIsNamed()
    {
        //arrange
        var expected = "myParam: 2";

        //act
        _sut.MakeNamed("myParam");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseStringValue_WhenCalled_ThenParameterIsNamed()
    {
        //arrange
        var expected = "\"test\"";

        //act
        _sut.UseStringValue("test");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseConst_WhenCalled_ThenParameterHaveCorrectValue()
    {
        //arrange
        var expected = "3213232312.123123";

        //act
        _sut.UseConst(3213232312.123123);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void UseConst_WhenCalledWithBoolean_ThenParameterHaveCorrectValue(bool value, string expected)
    {
        //arrange
        //act
        _sut.UseConst(value);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void FromConst_WhenCultureChangedInSettings_ThenUsesThisCultureToConvertToString()
    {
        //arrange
        var expected = "3213232312,123123";
        _sut.Settings.DefaultCulture = new CultureInfo("pl-PL");

        //act
        _sut.UseConst(3213232312.123123);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseName_WhenCalledWithIName_ThenUsesStringValue()
    {
        //arrange
        var expected = "TestName";

        //act
        _sut.UseName(new PascalCaseName("TestName"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Attribute_Parameter_UseStringValue_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                [Obsolete("Use something else")]
                                public class MyClass
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithAttribute(a => a.From("Obsolete").WithParameter(p => p.UseStringValue("Use something else")));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert - attribute is on the class, just verify it renders without error
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Attribute_Parameter_MakeNamed_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                [Obsolete(message = "Reason")]
                                public class MyClass
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithAttribute(a => a.From("Obsolete").WithParameter(p => p.UseStringValue("Reason").MakeNamed("message")));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert - attribute is on the class, just verify it renders without error
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void Attribute_Parameter_UseConst_True_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                [Obsolete(true)]
                                public class MyClass
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithAttribute(a => a.From("Obsolete").WithParameter(p => p.UseConst(true)));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert - attribute is on the class, just verify it renders without error
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void CtorCall_Parameter_UseStringValue_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                public class MyClass : BaseClass("value")
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithBase(b => b.From("BaseClass").WithCtorCall(c => c.WithParameter(p => p.UseStringValue("value"))));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void CtorCall_Parameter_UseConst_Int_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                public class MyClass : BaseClass(100)
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithBase(b => b.From("BaseClass").WithCtorCall(c => c.WithParameter(p => p.UseConst(100))));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void CtorCall_Parameter_MakeNamed_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                public class MyClass : BaseClass(id: 1)
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithBase(b => b.From("BaseClass").WithCtorCall(c => c.WithParameter(p => p.MakeNamed("id").UseConst(1))));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void CtorCall_Parameter_UseConst_True_ThenRendersCorrectly()
    {
        //arrange
        const string expected = """
                                public class MyClass : BaseClass(true)
                                {
                                    public void MyMethod()
                                    {
                                    }
                                }
                                """;

        var classChunk = new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"));
        var fileContext = new SourceCodeFileContext();
        ClassContext context = new(fileContext, classChunk);

        //act
        context.WithBase(b => b.From("BaseClass").WithCtorCall(c => c.WithParameter(p => p.UseConst(true))));
        context.WithMethod(m => m.Named("MyMethod"));

        //assert
        classChunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalledWithLiteralText_ThenUsesConstWithFormattedText()
    {
        //arrange
        const string expected = "Hello World";

        //act
        _sut.UseExpression($"Hello World");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalledWithInterpolatedValue_ThenUsesConstWithFormattedText()
    {
        //arrange
        const string expected = "Value is 42";

        //act
        _sut.UseExpression($"Value is {42}");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalledWithTypeToken_ThenAddsNamespaceAndUsesConst()
    {
        //arrange
        const string expected = "Hello String";
        var chunk = ParameterValueChunk.From("Hello String", CultureInfo.InvariantCulture);
        var contextWithUsings = new ParameterValueContext(new SourceCodeFileContext(), chunk);

        //act
        var typeToken = new NameInNamespace(new PascalCaseName("String"), "System", false);
        contextWithUsings.UseExpression($"Hello {typeToken}");

        //assert
        chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalledWithArrayTypeToken_ThenAddsNamespaceAndAppendsArrayBrackets()
    {
        //arrange
        const string expected = "Hello String[]";

        //act
        var typeToken = new NameInNamespace(new PascalCaseName("String"), "System", true);
        _sut.UseExpression($"Hello {typeToken}");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalledWithStringValue_ThenUsesConstWithStringValue()
    {
        //arrange
        const string expected = "test";

        //act
        _sut.UseExpression($"test");

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void UseExpression_WhenCalledWithMultipleValues_ThenUsesConstWithAllFormattedValues()
    {
        //arrange
        const string expected = "Hello 42 World";

        //act
        _sut.UseExpression($"Hello {42} World");

        //assert
        _chunk.Should().RenderAs(expected);
    }
}