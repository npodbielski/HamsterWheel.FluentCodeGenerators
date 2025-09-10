using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class PrimaryConstructorDefinitionContext(
    CodeBuilderContextBase previous,
    PrimaryConstructorDefinitionChunk ctorChunk)
    : CodeBuilderContextBase(previous), IPrimaryConstructorDefinitionContext
{
    public Dictionary<CamelCaseName, INamedChunk> Parameters => ctorChunk.Parameters;

    public IPrimaryConstructorDefinitionContext WithParameter(Action<IParameterDefinitionContext> configure)
    {
        var parameterDefChunk = new ParameterDefinitionChunk();
        configure(ParameterDefinitionContext.From(this, parameterDefChunk));
        parameterDefChunk.SetNameBasedOnType();

        ctorChunk.AddParameter(parameterDefChunk);
        return this;
    }

    public static IPrimaryConstructorDefinitionContext From(CodeBuilderContextBase previous,
        PrimaryConstructorDefinitionChunk callChunk) => new PrimaryConstructorDefinitionContext(previous, callChunk);
}

public static class PrimaryConstructorDefinitionContextExtensions
{
    public static IPrimaryConstructorDefinitionContext WithParameter<TParam>(
        this IPrimaryConstructorDefinitionContext context, string? name = null,
        Action<IParameterDefinitionContext>? configure = null)
    {
        context.WithParameter(p =>
        {
            if (name is not null)
            {
                p.Named(name);
            }

            p.OfType<TParam>();
            configure?.Invoke(p);
        });
        return context;
    }

    public static IPrimaryConstructorDefinitionContext ConfigureUsing<T>(
        this IPrimaryConstructorDefinitionContext context)
        where T : IContextConfigurator<IPrimaryConstructorDefinitionContext>, new() => context.ConfigureUsing(new T());
}