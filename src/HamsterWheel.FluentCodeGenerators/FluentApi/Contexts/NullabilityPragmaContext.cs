using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class NullabilityPragmaContext(CodeBuilderContextBase previous, PragmaChunk chunk)
    : CodeBuilderContextBase(previous), INullabilityPragmaContext
{
    public IContext Enable()
    {
        ((INullabilitySettings)this).Enabled = true;
        chunk.AddChunk(new EnableKeywordChunk());
        return this;
    }

    public IContext Disable()
    {
        ((INullabilitySettings)this).Enabled = false;
        chunk.AddChunk(new DisableKeywordChunk());
        return this;
    }
}