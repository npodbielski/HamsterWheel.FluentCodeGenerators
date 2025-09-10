using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class ITypeMemberWithTypeExtensions
{
    public static TContext OfType<TContext>(this TContext context, INameInNamespaceToken typeToken,
        Action<ITypeUsageContext>? configure = null)
        where TContext : ITypeMemberWithType<TContext>, IContext
    {
        context.OfType(t =>
        {
            t.From(typeToken);
            configure?.Invoke(t);
        });
        return context;
    }

    public static TContext OfType<TContext>(this TContext context, Type type,
        Action<ITypeUsageContext>? configure = null) where TContext : ITypeMemberWithType<TContext>, IContext
    {
        context.OfType(t =>
        {
            t.From(type);
            configure?.Invoke(t);
        });
        return context;
    }

    public static TContext OfType<TContext>(this TContext context, string type,
        Action<ITypeUsageContext>? configure = null) where TContext : ITypeMemberWithType<TContext>, IContext
    {
        context.OfType(t =>
        {
            t.From(new PascalCaseName(type));
            configure?.Invoke(t);
        });
        return context;
    }
}