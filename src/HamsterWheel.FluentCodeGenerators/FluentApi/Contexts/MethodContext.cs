using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class MethodContext(CodeBuilderContextBase previous, MethodDefinitionChunk methodChunk)
    : CodeBuilderContextBase(previous), IMethodContext
{
    public CamelCaseName[] ParametersNames => methodChunk.ParameterNames;

    public IMethodContext Named(string name)
    {
        methodChunk.ReplaceName(name);
        return this;
    }

    public IMethodContext WithBody(Action<IMethodBodyContext>? configure = null)
    {
        var context = MethodBodyContext.From(this, methodChunk.BodyChunks);
        configure?.Invoke(context);
        return this;
    }

    public IMethodContext WithExpressionBody(Action<IExpressionBodyContext>? configure = null)
    {
        methodChunk.AsExpressionBody();
        var context = ExpressionBodyContext.From(this, methodChunk.ExpressionBodyChunk, methodChunk.ParameterNames);
        configure?.Invoke(context);
        return this;
    }

    public IMethodContext WithParameter(Action<IParameterDefinitionContext> configure)
    {
        var paramDefChunk = new ParameterDefinitionChunk();
        configure(new ParameterDefinitionContext(this, paramDefChunk));
        paramDefChunk.SetNameBasedOnType();

        methodChunk.AddParameter(paramDefChunk);
        return this;
    }

    public IMethodContext MakeStatic()
    {
        methodChunk.MakeStatic();
        return this;
    }

    public IMethodContext SetVisibility(MemberVisibility visibility)
    {
        methodChunk.SetVisibility(visibility);
        return this;
    }

    public IMethodContext WithReturnType(Action<ITypeUsageContext> configure)
    {
        var typeChunk = new TypeNameChunk("void");
        methodChunk.SetReturnType(typeChunk);
        var context = TypeUsageContext.From(this, typeChunk);
        configure(context);
        return this;
    }

    public IMethodContext MakeOverride()
    {
        methodChunk.MakeOverride();
        return this;
    }

    public IMethodContext MakeSealed()
    {
        methodChunk.MakeSealed();
        return this;
    }

    public IMethodContext MakeAsync()
    {
        UsingsAppender.AddUsing<Task>();
        methodChunk.MakeAsync();
        return this;
    }

    public IMethodContext MakeVirtual()
    {
        methodChunk.MakeVirtual();
        return this;
    }

    public IMethodContext MakePartial()
    {
        methodChunk.MakePartial();
        return this;
    }

    public static IMethodContext From(CodeBuilderContextBase previous, MethodDefinitionChunk chunk) =>
        new MethodContext(previous, chunk);
}

public static class MethodContextExtensions
{
    public static IMethodContext WithBody<TConfigurator>(this IMethodContext context)
        where TConfigurator : IContextConfigurator<IMethodBodyContext>, new()
    {
        context.WithBody(new TConfigurator());
        return context;
    }

    public static IMethodContext WithBody(this IMethodContext context,
        IContextConfigurator<IMethodBodyContext> configurator)
    {
        context.WithBody(configurator.Configure);
        return context;
    }

    public static IMethodContext WithCancellation(this IMethodContext context) =>
        context.WithParameter<CancellationToken>((PascalCaseName)nameof(CancellationToken).ToCamelCase(), p => p.PushToEnd());

    public static IMethodContext WithParameter<TParam>(this IMethodContext context, IName name,
        Action<IParameterDefinitionContext>? configure = null)
    {
        context.AddUsing<TParam>();
        return context.WithParameter(name, typeof(TParam).Name, configure);
    }

    public static IMethodContext WithParameter(this IMethodContext context, IName name, INameInNamespaceToken type,
        Action<IParameterDefinitionContext>? configure = null)
    {
        context.AddUsing(type);
        return context.WithParameter(name, type.Name, configure);
    }

    public static IMethodContext WithParameter(this IMethodContext context, IName name, Type type,
        Action<IParameterDefinitionContext>? configure = null)
    {
        context.AddUsing(type);
        return context.WithParameter(name, type.Name, configure);
    }

    public static IMethodContext WithParameter(this IMethodContext context, IName name, string? type = null,
        Action<IParameterDefinitionContext>? configure = null)
    {
        return context.WithParameter(p =>
        {
            p.Named(name);
            if (type != null)
            {
                p.OfType(type);
            }

            configure?.Invoke(p);
        });
    }

    public static IMethodContext WithReturnType<TReturn>(this IMethodContext context,
        Action<ITypeUsageContext>? configure = null)
    {
        context.WithReturnType(typeof(TReturn), configure);
        return context;
    }

    public static IMethodContext WithReturnType(this IMethodContext context, Type type,
        Action<ITypeUsageContext>? configure = null)
    {
        context.WithReturnType(t =>
        {
            t.From(type);
            configure?.Invoke(t);
        });
        return context;
    }

    public static IMethodContext WithReturnType(this IMethodContext context, INameInNamespaceToken name,
        Action<ITypeUsageContext>? configure = null)
    {
        context.AddUsing(name.Namespace);
        context.WithReturnType(t =>
        {
            t.From(name);
            configure?.Invoke(t);
        });
        return context;
    }

    public static IMethodContext WithReturnType(this IMethodContext context, IName name,
        Action<ITypeUsageContext>? configure = null)
    {
        context.WithReturnType(t =>
        {
            t.From(name);
            configure?.Invoke(t);
        });
        return context;
    }

    public static IMethodContext WithReturnType(this IMethodContext context, string name,
        Action<ITypeUsageContext>? configure = null)
    {
        context.WithReturnType(t =>
        {
            t.From(new PascalCaseName(name));
            configure?.Invoke(t);
        });
        return context;
    }
}