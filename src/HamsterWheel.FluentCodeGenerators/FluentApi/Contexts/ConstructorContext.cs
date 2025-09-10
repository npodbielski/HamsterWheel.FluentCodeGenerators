using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ConstructorContext(CodeBuilderContextBase previous, CtorDefinitionChunk chunk)
    : CodeBuilderContextBase(previous), IConstructorContext
{
    public CamelCaseName[] ParametersNames => chunk.ParameterNames;

    public IConstructorContext WithBody(Action<IMethodBodyContext>? configure = null)
    {
        var context = MethodBodyContext.From(this, chunk.BodyChunks);
        configure?.Invoke(context);
        return this;
    }

    public IConstructorContext WithExpressionBody(Action<IExpressionBodyContext>? configure = null)
    {
        chunk.AsExpressionBody();
        var context = ExpressionBodyContext.From(this, chunk.ExpressionBodyChunk, ParametersNames);
        configure?.Invoke(context);
        return this;
    }

    public IConstructorContext WithParameter(Action<IParameterDefinitionContext> configure)
    {
        var paramDefChunk = new ParameterDefinitionChunk();
        configure(new ParameterDefinitionContext(this, paramDefChunk));
        chunk.AddParameter(paramDefChunk);
        return this;
    }

    public static ConstructorContext From(CodeBuilderContextBase previous, CtorDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class ConstructorContextExtensions
{
    public static ConstructorContext ConfigureUsing<T>(this ConstructorContext context)
        where T : IContextConfigurator<ConstructorContext>, new() => context.ConfigureUsing(new T());
}