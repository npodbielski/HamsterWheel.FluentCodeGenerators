using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Tokens;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class MethodContextExtensionsUnitTests
{
    private readonly MethodContext _sut;
    private readonly MethodDefinitionChunk _chunk;

    public MethodContextExtensionsUnitTests()
    {
        _chunk = new MethodDefinitionChunk("NewMethod");
        _sut = new MethodContext(new SourceCodeFileContext(), _chunk);
    }

    [Fact]
    public void WithBody_WhenCalledWithConfigurator_ThenAddsBody()
    {
        //arrange
        var expected = """
                       public void NewMethod()
                       {
                           //This is comment in my method
                       }
                       """;

        //act
        _sut.WithBody(new MethodBodyConfigurator());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithBodyOfT1_WhenCalledWithTypeOfConfigurator_ThenAddsBody()
    {
        //arrange
        var expected = """
                       public void NewMethod()
                       {
                           //This is comment in my method
                       }
                       """;

        //act
        _sut.WithBody<MethodBodyConfigurator>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCancellation_WhenCalled_ThenAddsCancellationToken()
    {
        //arrange
        var expected = """
                       public void NewMethod(CancellationToken cancellationToken)
                       {
                       }
                       """;

        //act
        _sut.WithCancellation();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithCancellation_WhenCalled_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System.Threading;
                       """;

        //act
        _sut.WithCancellation();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameterT1_WhenCalled_ThenAddsCorrectParameter()
    {
        //arrange
        var expected = """
                       public void NewMethod(int newParam)
                       {
                       }
                       """;

        //act
        _sut.WithParameter<int>("newParam".ToPascalCaseName());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameterT1_WhenCalled_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System;
                       """;

        //act
        _sut.WithParameter<int>("newParam".ToPascalCaseName());

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalledWithNameAndNameInNamespace_ThenAddsCorrectParameter()
    {
        //arrange
        var expected = """
                       public void NewMethod(int newParam)
                       {
                       }
                       """;

        //act
        _sut.WithParameter("newParam".ToCamelCaseName(), NameInNamespace.From("int", "System"));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalledWithNameAndNameInNamespace_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System.Net;
                       """;

        //act
        _sut.WithParameter("newParam".ToCamelCaseName(), NameInNamespace.From("IPAddressInformation", "System.Net"));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalledWithNameAndType_ThenAddsCorrectParameter()
    {
        //arrange
        var expected = """
                       public void NewMethod(int newParam)
                       {
                       }
                       """;

        //act
        _sut.WithParameter("newParam".ToCamelCaseName(), typeof(int));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithParameter_WhenCalledWithNameAndType_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using System.Linq;
                       """;

        //act
        _sut.WithParameter("newParam".ToCamelCaseName(), typeof(IQueryable));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnTypeOfT1_WhenCalled_ThenAddsReturnType()
    {
        //arrange
        var expected = """
                       public DateTime NewMethod()
                       {
                       }
                       """;

        //act
        _sut.WithReturnType<DateTime>();

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnTypeOfT1_WhenCalled_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using Xunit;
                       """;

        //act
        _sut.WithReturnType<IAsyncLifetime>();

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnType_WhenCalledWithType_ThenAddsReturnType()
    {
        //arrange
        var expected = """
                       public string NewMethod()
                       {
                       }
                       """;

        //act
        _sut.WithReturnType(typeof(string));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnType_WhenCalledWithType_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
                       """;

        //act
        _sut.WithReturnType(typeof(ISourceCodeFileContext));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnType_WhenCalledWithNameInNamespace_ThenAddsReturnType()
    {
        //arrange
        var expected = """
                       public IContext NewMethod()
                       {
                       }
                       """;

        //act
        _sut.WithReturnType("IContext".ToPascalCaseName()
            .InNamespace("HamsterWheel.FluentCodeGenerators.FluentApi.Contexts".ToNamespace()));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnType_WhenCalledWithNameInNamespace_ThenAddsUsing()
    {
        //arrange
        var expected = """
                       using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
                       """;

        //act
        _sut.WithReturnType("IContext".ToPascalCaseName()
            .InNamespace("HamsterWheel.FluentCodeGenerators.FluentApi.Contexts".ToNamespace()));

        //assert
        _sut.Usings.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnType_WhenCalledWithName_ThenAddsReturnType()
    {
        //arrange
        var expected = """
                       public test NewMethod()
                       {
                       }
                       """;

        //act
        _sut.WithReturnType((IName)"test".ToPascalCaseName());

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void WithReturnType_WhenCalledWithString_ThenAddsReturnType()
    {
        //arrange
        var expected = """
                       public test NewMethod()
                       {
                       }
                       """;

        //act
        _sut.WithReturnType("test");

        //assert
        _chunk.Should().RenderAs(expected);
    }
}

public class MethodBodyConfigurator : IContextConfigurator<IMethodBodyContext>
{
    public void Configure(IMethodBodyContext context)
    {
        context.AppendComment("This is comment in my method");
    }
}