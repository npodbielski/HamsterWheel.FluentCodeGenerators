using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IInheritingTypeExtensions
{
    public static TContext WithBase<TContext>(this TContext context, INamedTypeSymbol name,
        Action<IImplementationsWithPrimaryCtorContext>? configure = null) where TContext : IInheritingType<IContext>, IContext
    {
        context.WithBase(b =>
        {
            b.From(name);
            configure?.Invoke(b);
        });
        return context;
    }

    public static TContext WithBase<TContext>(this TContext context, INameInNamespaceToken name,
        Action<IImplementationsWithPrimaryCtorContext>? configure = null) where TContext : IInheritingType<IContext>, IContext
    {
        context.WithBase(b =>
        {
            b.From(name);
            configure?.Invoke(b);
        });
        return context;
    }

    public static TContext WithBase<TContext>(this TContext context, string name, string @namespace,
        Action<IImplementationsWithPrimaryCtorContext>? configure = null) where TContext : IInheritingType<IContext>, IContext
    {
        context.WithBase(b =>
        {
            b.AddUsing(@namespace);
            b.From(name);
            configure?.Invoke(b);
        });
        return context;
    }
}