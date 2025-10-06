using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class ThisCtorChunk(ParametersValuesListChunk? parameters = null) : CtorChainChunk(new ThisKeywordChunk(), parameters);