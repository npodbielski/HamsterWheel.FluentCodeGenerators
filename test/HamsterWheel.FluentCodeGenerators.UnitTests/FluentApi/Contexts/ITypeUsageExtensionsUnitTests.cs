using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.FluentApi.Contexts;

public class ITypeUsageExtensionsUnitTests
{
    private readonly TypeNameChunk _chunk;
    private readonly TypeUsageContext _sut;

    public ITypeUsageExtensionsUnitTests()
    {
        _chunk = TypeNameChunk.From<int>();
        var context = new SourceCodeFileContext();
        _sut = new TypeUsageContext(context, _chunk);
    }

    [Fact]
    public void From_WhenCalledWithNullableTypeAndTypeUsageContext_ThenUsesArgumentAsType()
    {
        //arrange
        var expected = "int?";

        //act
        _sut.From(typeof(int?));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalledWithGenericType_ThenUsesArgumentAsType()
    {
        //arrange
        var expected = "List<int>";

        //act
        _sut.From(typeof(List<int>));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalledWithGenericTypeWith2Arguments_ThenUsesArgumentAsType()
    {
        //arrange
        var expected = "Dictionary<string, int>";

        //act
        _sut.From(typeof(Dictionary<string, int>));

        //assert
        _chunk.Should().RenderAs(expected);
    }

    [Fact]
    public void From_WhenCalledWithNullableTypeAndImplementationsContext_ThenUsesArgumentAsType()
    {
        //arrange
        var sut = new ImplementationsWithPrimaryCtorContext<ClassContext>(
            new ClassContext(new SourceCodeFileContext(),
                new ClassDefinitionChunk(TypeDefinitionWithPrimaryConstructorChunk.FromName("MyClass"))),
            new ImplementationsListChunk());
        var action = () => sut.From(typeof(int?));

        //act
        action.Should()
            .Throw<IncorrectTypeUsageContextException<ITypeUsageContext,
                ImplementationsWithPrimaryCtorContext<ClassContext>>>();

        //assert
    }
}