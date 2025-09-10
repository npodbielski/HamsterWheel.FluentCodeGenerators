using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class PrimaryConstructorCallContext(CodeBuilderContextBase previous, PrimaryConstructorCallChunk callChunk)
    : CodeBuilderContextBase(previous), IPrimaryConstructorCallContext
{
    public CamelCaseName[] ParametersNames => callChunk.Parameters.Select(p => new CamelCaseName(p.Name)).ToArray();

    public IPrimaryConstructorCallContext WithParameter(Action<IParameterValueContext> configure)
    {
        var parameterValueChunk = ParameterValueChunk.From("");
        callChunk.AddParameter(parameterValueChunk);
        var context = ParameterValueContext.From(this, parameterValueChunk);
        configure(context);
        return this;
    }

    public static PrimaryConstructorCallContext From(CodeBuilderContextBase previous,
        PrimaryConstructorCallChunk callChunk) => new(previous, callChunk);
}