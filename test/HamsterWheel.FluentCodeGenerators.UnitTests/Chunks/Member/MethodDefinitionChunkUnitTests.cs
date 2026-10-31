using System.Text;
using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Chunks.Member;

public class MethodDefinitionChunkUnitTests
{
    [Fact]
    public void Build_WhenCalledWithBasicMethod_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("TestMethod");
        chunk.SetReturnType(TypeNameChunk.Void);

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public void TestMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("GetDateTime");
        chunk.SetReturnType(TypeNameChunk.From<DateTime>());

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public DateTime GetDateTime()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SetValue");
        chunk.SetReturnType(TypeNameChunk.Void);
        var param = new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "value");
        chunk.AddParameter(param);

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public void SetValue(int value)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithMultipleParameters_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("Combine");
        chunk.SetReturnType(TypeNameChunk.From<string>());
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<string>(), "a"));
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<string>(), "b"));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public string Combine(string a, string b)\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithBody_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DoSomething");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.BodyChunks.Append("Console.WriteLine(\"hello\");");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public void DoSomething()
                           {
                               Console.WriteLine("hello");
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithExpressionBody_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("GetResult");
        chunk.SetReturnType(TypeNameChunk.From<int>());
        chunk.AsExpressionBody();
        chunk.ExpressionBodyChunk.Append("42;");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public int GetResult() => 42;");
    }

    [Fact]
    public void Build_WhenCalledWithVirtualKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public virtual void VirtualMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithStaticKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public static void StaticMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithOverrideKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public override void OverrideMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithSealedKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SealedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeSealed();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public sealed override void SealedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithAsyncKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncMethod");
        chunk.MakeAsync();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public async Task AsyncMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithAsyncValueTask_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskMethod");
        chunk.MakeAsyncValueTask();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public async ValueTask AsyncValueTaskMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithAsyncAndReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("GetResultAsync");
        chunk.SetReturnType(TypeNameChunk.From<int>());
        chunk.MakeAsync();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public async Task<int> GetResultAsync()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithPartialKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("PartialMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakePartial();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("partial void PartialMethod();\n");
    }

    [Fact]
    public void Build_WhenCalledWithAbstractKeyword_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AbstractMethod");
        chunk.MakeAbstract();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public abstract void AbstractMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithReplaceName_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OriginalName");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.ReplaceName("NewName");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public void NewName()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithSuppressEmit_ThenReturnsFalse()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SuppressedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.SuppressEmit();

        //act
        var target = new StringBuilder();
        var result = chunk.AppendChunks(target);

        //assert
        result.Should().BeFalse();
        target.ToString().Should().BeEmpty();
    }

    [Fact]
    public void Build_WhenCalledWithVirtualAndStatic_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualStaticMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeStatic();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public virtual static void VirtualStaticMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithVirtualAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualSealedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeSealed();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public sealed override void VirtualSealedMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithOverrideAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideSealedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakeSealed();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public sealed override void OverrideSealedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithNoReturnType_ThenReturnsVoid()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("NoReturnType");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public void NoReturnType()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithNullableReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("NullableMethod");
        var returnType = TypeNameChunk.From<int>();
        returnType.MakeNullable();
        chunk.SetReturnType(returnType);

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public int? NullableMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndBody_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedMethodWithBody");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddComment(new SummaryCommentChunk("This is documented."));
        chunk.BodyChunks.Append("Console.WriteLine(\"hello\");");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// This is documented.
                           /// </summary>
                           public void DocumentedMethodWithBody()
                           {
                               Console.WriteLine("hello");
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithExpressionBodyAndComment_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedExpressionMethod");
        chunk.SetReturnType(TypeNameChunk.From<int>());
        chunk.AsExpressionBody();
        chunk.ExpressionBodyChunk.Append("42;");
        chunk.AddComment(new SummaryCommentChunk("Returns 42."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Returns 42.
                           /// </summary>
                           public int DocumentedExpressionMethod() => 42;
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithParameterNames_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("MethodWithParams");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "first"));
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<string>(), "second"));

        //act
        var names = chunk.ParameterNames;

        //assert
        names.Should().HaveCount(2);
        names[0].ToString().Should().Be("first");
        names[1].ToString().Should().Be("second");
    }

    [Fact]
    public void Build_WhenCalledWithRawReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("MethodWithRawReturn");
        var returnType = TypeNameChunk.From<DateTime>();
        chunk.SetReturnType(returnType);

        //act
        var rawReturn = chunk.RawReturnType;

        //assert
        rawReturn.Should().NotBeNull();
        rawReturn!.Name.NameAsString.Should().Be("DateTime");
    }

    [Fact]
    public void Build_WhenCalledWithParametersList_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("MethodWithParamsList");
        chunk.SetReturnType(TypeNameChunk.Void);
        var param1 = new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "a");
        var param2 = new ParameterDefinitionChunk(TypeNameChunk.From<string>(), "b");
        chunk.AddParameter(param1);
        chunk.AddParameter(param2);

        //act
        var paramsList = chunk.ParametersList;

        //assert
        paramsList.Should().HaveCount(2);
        paramsList[0].Name.ToString().Should().Be("a");
        paramsList[1].Name.ToString().Should().Be("b");
    }

    [Fact]
    public void Build_WhenCalledWithAsyncAndNullableReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncNullableMethod");
        var returnType = TypeNameChunk.From<int>();
        returnType.MakeNullable();
        chunk.SetReturnType(returnType);
        chunk.MakeAsync();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public async Task<int?> AsyncNullableMethod()\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithAsyncValueTaskAndReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskMethodWithReturn");
        chunk.SetReturnType(TypeNameChunk.From<string>());
        chunk.MakeAsyncValueTask();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           public async ValueTask<string> AsyncValueTaskMethodWithReturn()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithAbstractAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AbstractPartialMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakePartial();

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("partial void AbstractPartialMethod();\n");
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsync_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncDocumentedMethod");
        chunk.MakeAsync();
        chunk.AddComment(new SummaryCommentChunk("Async method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async method.
                           /// </summary>
                           public async Task AsyncDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithBodyAndExpressionBody_ThenReturnsBody()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("MethodWithBothBodies");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.BodyChunks.Append("body;");
        chunk.ExpressionBodyChunk.Append("expr;");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("public void MethodWithBothBodies()\n{\n    body;\n}");
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("PartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Partial method.
                           /// </summary>
                           partial void PartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddComment(new SummaryCommentChunk("method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// method.
                           /// </summary>
                           public void DocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverride_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override method.
                           /// </summary>
                           public override void OverrideDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtual_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.AddComment(new SummaryCommentChunk("Virtual method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual method.
                           /// </summary>
                           public virtual void VirtualDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Sealed method.
                           /// </summary>
                           public sealed override void SealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStatic_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.AddComment(new SummaryCommentChunk("Static method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static method.
                           /// </summary>
                           public static void StaticDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTask_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskDocumentedMethod");
        chunk.MakeAsyncValueTask();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask method.
                           /// </summary>
                           public async ValueTask AsyncValueTaskDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.AddComment(new SummaryCommentChunk("Method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Method with param.
                           /// </summary>
                           public void DocumentedMethodWithParam(int param)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndReturnType_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedMethodWithReturn");
        chunk.SetReturnType(TypeNameChunk.From<DateTime>());
        chunk.AddComment(new SummaryCommentChunk("Method with return."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Method with return.
                           /// </summary>
                           public DateTime DocumentedMethodWithReturn()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndBodyAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedMethodWithBodyAndParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.AddComment(new SummaryCommentChunk("Method with body and param."));
        chunk.BodyChunks.Append("Console.WriteLine(param);");

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Method with body and param.
                           /// </summary>
                           public void DocumentedMethodWithBodyAndParam(int param)
                           {
                               Console.WriteLine(param);
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndExpressionBodyAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("DocumentedExpressionMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.From<int>());
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.AsExpressionBody();
        chunk.ExpressionBodyChunk.Append("param;");
        chunk.AddComment(new SummaryCommentChunk("Expression method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Expression method with param.
                           /// </summary>
                           public int DocumentedExpressionMethodWithParam(int param) => param;
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncDocumentedMethodWithParam");
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.MakeAsync();
        chunk.AddComment(new SummaryCommentChunk("Async method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async method with param.
                           /// </summary>
                           public async Task AsyncDocumentedMethodWithParam(int param)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticDocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.MakeStatic();
        chunk.AddComment(new SummaryCommentChunk("Static method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static method with param.
                           /// </summary>
                           public static void StaticDocumentedMethodWithParam(int param)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideDocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Override method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should()
            .Be(
                "/// <summary>\n/// Override method with param.\n/// </summary>\npublic override void OverrideDocumentedMethodWithParam(int param)\n{\n}");
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndSealedAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SealedDocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Sealed method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Sealed method with param.
                           /// </summary>
                           public sealed override void SealedDocumentedMethodWithParam(int param)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualDocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.MakeVirtual();
        chunk.AddComment(new SummaryCommentChunk("Virtual method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual method with param.
                           /// </summary>
                           public virtual void VirtualDocumentedMethodWithParam(int param)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndPartialAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("PartialDocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Partial method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Partial method with param.
                           /// </summary>
                           partial void PartialDocumentedMethodWithParam(int param);

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAbstractAndParameter_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AbstractDocumentedMethodWithParam");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.AddParameter(new ParameterDefinitionChunk(TypeNameChunk.From<int>(), "param"));
        chunk.AddComment(new SummaryCommentChunk("Abstract method with param."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Abstract method with param.
                           /// </summary>
                           public void AbstractDocumentedMethodWithParam(int param)
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndStatic_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncStaticDocumentedMethod");
        chunk.MakeAsync();
        chunk.MakeStatic();
        chunk.AddComment(new SummaryCommentChunk("Async static method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async static method.
                           /// </summary>
                           public static async Task AsyncStaticDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndVirtual_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncVirtualDocumentedMethod");
        chunk.MakeAsync();
        chunk.MakeVirtual();
        chunk.AddComment(new SummaryCommentChunk("Async virtual method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async virtual method.
                           /// </summary>
                           public virtual async Task AsyncVirtualDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndOverride_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncOverrideDocumentedMethod");
        chunk.MakeAsync();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Async override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async override method.
                           /// </summary>
                           public override async Task AsyncOverrideDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncSealedDocumentedMethod");
        chunk.MakeAsync();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Async sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async sealed method.
                           /// </summary>
                           public sealed override async Task AsyncSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncPartialDocumentedMethod");
        chunk.MakeAsync();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Async partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async partial method.
                           /// </summary>
                           partial async Task AsyncPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncAbstractDocumentedMethod");
        chunk.MakeAsync();
        chunk.AddComment(new SummaryCommentChunk("Async method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async method.
                           /// </summary>
                           public async Task AsyncAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTaskAndStatic_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskStaticDocumentedMethod");
        chunk.MakeAsyncValueTask();
        chunk.MakeStatic();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask static method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask static method.
                           /// </summary>
                           public static async ValueTask AsyncValueTaskStaticDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTaskAndVirtual_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskVirtualDocumentedMethod");
        chunk.MakeAsyncValueTask();
        chunk.MakeVirtual();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask virtual method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask virtual method.
                           /// </summary>
                           public virtual async ValueTask AsyncValueTaskVirtualDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTaskAndOverride_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskOverrideDocumentedMethod");
        chunk.MakeAsyncValueTask();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask override method.
                           /// </summary>
                           public override async ValueTask AsyncValueTaskOverrideDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTaskAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskSealedDocumentedMethod");
        chunk.MakeAsyncValueTask();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask sealed method.
                           /// </summary>
                           public sealed override async ValueTask AsyncValueTaskSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTaskAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskPartialDocumentedMethod");
        chunk.MakeAsyncValueTask();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask partial method.
                           /// </summary>
                           partial async ValueTask AsyncValueTaskPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndAsyncValueTaskAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("AsyncValueTaskAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.From<ValueTask>());
        chunk.MakeAsyncValueTask();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Async ValueTask method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Async ValueTask method.
                           /// </summary>
                           public abstract async ValueTask<ValueTask> AsyncValueTaskAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtual_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.AddComment(new SummaryCommentChunk("Static virtual method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual method.
                           /// </summary>
                           public virtual void StaticVirtualDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverride_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverrideDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Static override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static override method.
                           /// </summary>
                           public override static void StaticOverrideDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed method.
                           /// </summary>
                           public sealed override void StaticSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static partial method.
                           /// </summary>
                           partial void StaticPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeAbstract();
        chunk.MakeStatic();
        chunk.AddComment(new SummaryCommentChunk("Static method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should()
            .Be(
                "/// <summary>\n/// Static method.\n/// </summary>\npublic abstract static void StaticAbstractDocumentedMethod();\n");
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverride_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverrideDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Virtual override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual override method.
                           /// </summary>
                           public virtual override void VirtualOverrideDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed method.
                           /// </summary>
                           public sealed override void VirtualSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Virtual partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual partial method.
                           /// </summary>
                           partial void VirtualPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.AddComment(new SummaryCommentChunk("Virtual method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual method.
                           /// </summary>
                           public virtual void VirtualAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Override sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override sealed method.
                           /// </summary>
                           public sealed override void OverrideSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverridePartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override partial method.
                           /// </summary>
                           partial void OverridePartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override method.
                           /// </summary>
                           public abstract override void OverrideAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Sealed partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should()
            .Be(
                """
                /// <summary>
                /// Sealed partial method.
                /// </summary>
                partial void SealedPartialDocumentedMethod();

                """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeSealed();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Sealed method.
                           /// </summary>
                           public abstract override void SealedAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("PartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeAbstract();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Partial method.
                           /// </summary>
                           partial void PartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndOverride_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverrideDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Static virtual override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual override method.
                           /// </summary>
                           public virtual override void StaticVirtualOverrideDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed method.
                           /// </summary>
                           public sealed override void StaticVirtualSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual partial method.
                           /// </summary>
                           partial void StaticVirtualPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Static virtual method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual method.
                           /// </summary>
                           public abstract void StaticVirtualAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverrideSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static sealed abstract override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed abstract override method.
                           /// </summary>
                           public sealed override void StaticOverrideSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverridePartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static override partial method.
                           /// </summary>
                           partial void StaticOverridePartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverrideAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Static override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static override method.
                           /// </summary>
                           public override static void StaticOverrideAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed abstract partial method.
                           /// </summary>
                           partial void StaticSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed method.
                           /// </summary>
                           public sealed override void StaticSealedAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeAbstract();
        chunk.MakeStatic();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static partial method.
                           /// </summary>
                           partial void StaticPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverrideAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverrideSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed abstract override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed abstract override method.
                           /// </summary>
                           public sealed override void VirtualOverrideSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverrideAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverridePartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Virtual override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual override partial method.
                           /// </summary>
                           partial void VirtualOverridePartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverrideAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverrideAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Virtual override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual override method.
                           /// </summary>
                           public virtual override void VirtualOverrideAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed abstract partial method.
                           /// </summary>
                           partial void VirtualSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed method.
                           /// </summary>
                           public abstract override void VirtualSealedAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeAbstract();
        chunk.MakeVirtual();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Virtual partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual partial method.
                           /// </summary>
                           partial void VirtualPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Override sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override sealed abstract partial method.
                           /// </summary>
                           partial void OverrideSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Override sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override sealed method.
                           /// </summary>
                           public abstract override void OverrideSealedAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverridePartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeAbstract();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override partial method.
                           /// </summary>
                           partial void OverridePartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("SealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeAbstract();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Sealed partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Sealed partial method.
                           /// </summary>
                           partial void SealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndSealed_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverrideSealedDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed abstract override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed abstract override method.
                           /// </summary>
                           public sealed override void StaticVirtualOverrideSealedDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverridePartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual override partial method.
                           /// </summary>
                           partial void StaticVirtualOverridePartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverrideAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.AddComment(new SummaryCommentChunk("Static virtual override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual override method.
                           /// </summary>
                           public virtual override void StaticVirtualOverrideAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed abstract partial method.
                           /// </summary>
                           partial void StaticVirtualSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed method.
                           /// </summary>
                           public sealed override void StaticVirtualSealedAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual partial method.
                           /// </summary>
                           partial void StaticVirtualPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverrideSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static sealed abstract override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed abstract override partial method.
                           /// </summary>
                           partial void StaticOverrideSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverrideSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Static sealed abstract override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed abstract override method.
                           /// </summary>
                           public abstract override void StaticOverrideSealedAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverridePartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static override partial method.
                           /// </summary>
                           partial void StaticOverridePartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed abstract partial method.
                           /// </summary>
                           partial void StaticSealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverrideAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverrideSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed abstract override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed abstract override partial method.
                           /// </summary>
                           partial void VirtualOverrideSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverrideAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverrideSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed abstract override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed abstract override method.
                           /// </summary>
                           public sealed override void VirtualOverrideSealedAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndOverrideAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverridePartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Virtual override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual override partial method.
                           /// </summary>
                           public abstract void VirtualOverridePartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndVirtualAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed abstract partial method.
                           /// </summary>
                           public abstract void VirtualSealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndOverrideAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("OverrideSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Override sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Override sealed abstract partial method.
                           /// </summary>
                           public abstract void OverrideSealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndSealedAndPartial_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverrideSealedPartialDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed abstract override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed abstract override partial method.
                           /// </summary>
                           partial void StaticVirtualOverrideSealedPartialDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndSealedAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverrideSealedAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed abstract override method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed abstract override method.
                           /// </summary>
                           public sealed override void StaticVirtualOverrideSealedAbstractDocumentedMethod()
                           {
                           }
                           """);
    }

    [Fact]
    public void
        Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverridePartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual override partial method.
                           /// </summary>
                           partial void StaticVirtualOverridePartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndVirtualAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.MakeAbstract();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed abstract partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed abstract partial method.
                           /// </summary>
                           public abstract void StaticVirtualSealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void Build_WhenCalledWithCommentAndStaticAndOverrideAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticOverrideSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static sealed abstract override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static sealed abstract override partial method.
                           /// </summary>
                           partial void StaticOverrideSealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void
        Build_WhenCalledWithCommentAndVirtualAndOverrideAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("VirtualOverrideSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Virtual sealed abstract override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Virtual sealed abstract override partial method.
                           /// </summary>
                           partial void VirtualOverrideSealedPartialAbstractDocumentedMethod();

                           """);
    }

    [Fact]
    public void
        Build_WhenCalledWithCommentAndStaticAndVirtualAndOverrideAndSealedAndPartialAndAbstract_ThenReturnsCorrectOutput()
    {
        //arrange
        var chunk = new MethodDefinitionChunk("StaticVirtualOverrideSealedPartialAbstractDocumentedMethod");
        chunk.SetReturnType(TypeNameChunk.Void);
        chunk.MakeStatic();
        chunk.MakeVirtual();
        chunk.MakeOverride();
        chunk.MakeSealed();
        chunk.MakePartial();
        chunk.AddComment(new SummaryCommentChunk("Static virtual sealed abstract override partial method."));

        //act
        var actual = chunk.Build();

        //assert
        actual.Should().Be("""
                           /// <summary>
                           /// Static virtual sealed abstract override partial method.
                           /// </summary>
                           partial void StaticVirtualOverrideSealedPartialAbstractDocumentedMethod();

                           """);
    }
}