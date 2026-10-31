using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public sealed class BaseCtorChunk(ParametersValuesListChunk? parameters = null)
    : CtorChainChunk(new BaseKeywordChunk(), parameters);