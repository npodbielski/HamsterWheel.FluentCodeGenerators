using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using HamsterWheel.FluentCodeGenerators.Chunks;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Assertions;

public class ICodeChunkAssertions<T>(T chunk)
    : ReferenceTypeAssertions<ICodeChunk, ICodeChunkAssertions<T>>(chunk, AssertionChain.GetOrCreate())
    where T : ICodeChunk
{
    protected override string Identifier => nameof(ICodeChunk);

    public AndConstraint<ICodeChunkAssertions<T>> RenderAs(string expected, bool noTrim = false)
    {
        var codeFileChunk = new CodeFileChunks();
        codeFileChunk.AddType(chunk);
        var rendered = (string)codeFileChunk;
        if (!noTrim)
        {
            rendered = rendered.Trim();
        }
        rendered.Should().Be(expected);

        return new AndConstraint<ICodeChunkAssertions<T>>(this);
    }
}