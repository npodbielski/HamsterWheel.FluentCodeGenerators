using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class MethodContextUnitTests
{
    private readonly MethodContext _sut;
    private readonly MethodDefinitionChunk _chunk;

    public MethodContextUnitTests()
    {
        _chunk = new MethodDefinitionChunk("NewMethod");
        _sut = new MethodContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithBody_WhenCalled_ThenAddsBody()
    {
        //arrange
        var expected = """
                       public void NewMethod()
                       {
                           //this is comment in my method
                           Console.WriteLine(this.ToString());
                       }
                       """;

        //act
        _sut.WithBody(b=>
        {
            b.Append("//this is comment in my method");
            b.AppendLine();
            b.Append("Console.WriteLine(this.ToString());");
        });

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithExpressionBody_WhenCalled_ThenAddsBody()
    {
        //arrange
        var expected = """
                       public void NewMethod() => Console.WriteLine(this.ToString());
                       """;

        //act
        _sut.WithExpressionBody(b=> b.Append("Console.WriteLine(this.ToString());"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeStatic_WhenCalled_ThenMakesMethodStatic()
    {
        //arrange
        var expected = """
                       public static void NewMethod()
                       {
                       }
                       """;

        //act
        _sut.MakeStatic();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Theory]
    [InlineData(MemberVisibility.Internal, "internal")]
    [InlineData(MemberVisibility.Private, "private")]
    [InlineData(MemberVisibility.PrivateProtected, "protected private")]
    [InlineData(MemberVisibility.Protected, "protected")]
    [InlineData(MemberVisibility.ProtectedInternal, "protected internal")]
    [InlineData(MemberVisibility.Public, "public")]
    public void SetVisibility_WhenCalled_ThenHaveCorrectVisibilityModifier(MemberVisibility visibility, string expectedVisibilityModifier)
    {
        //arrange
        var expected = $$"""
                       {{expectedVisibilityModifier}} void NewMethod()
                       {
                       }
                       """;

        //act
        _sut.SetVisibility(visibility);

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeOverride_WhenCalled_ThenMakesMethodOverride()
    {
        //arrange
        var expected = """
                       public override void NewMethod()
                       {
                       }
                       """;

        //act
        _sut.MakeOverride();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeSealed_WhenCalled_ThenMakesMethodSealed()
    {
        //arrange
        var expected = """
                       public sealed void NewMethod()
                       {
                       }
                       """;

        //act
        _sut.MakeSealed();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeAsync_WhenCalledOnVoid_ThenMakesMethodAsyncTask()
    {
        //arrange
        var expected = """
                       public async Task NewMethod()
                       {
                       }
                       """;

        //act
        _sut.MakeAsync();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeAsync_WhenCalledOnMethodWithReturnType_ThenMakesMethodAsyncTaskWithGenericParameter()
    {
        //arrange
        var expected = """
                       public async Task<string> NewMethod()
                       {
                       }
                       """;

        //act
        _sut.WithReturnType<string>().MakeAsync();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakeVirtual_WhenCalled_ThenMakesMethodVirtual()
    {
        //arrange
        var expected = """
                       public virtual void NewMethod()
                       {
                       }
                       """;

        //act
        _sut.MakeVirtual();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void MakePartial_WhenCalled_ThenMakesMethodPartial()
    {
        //arrange
        var expected = """
                       partial void NewMethod();
                       """;

        //act
        _sut.MakePartial();

        //assert
        _chunk.Should().RenderAs(expected);
    }
}