using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class PragmaContext(CodeBuilderContextBase previous, PragmaChunk chunk)
    : CodeBuilderContextBase(previous), IPragmaContext
{
    public IContext Nullability(Action<INullabilityPragmaContext> configure)
    {
        chunk.AddChunk(new NullableKeywordChunk());
        var context = new NullabilityPragmaContext(this, chunk);
        configure(context);
        return this;
    }
}