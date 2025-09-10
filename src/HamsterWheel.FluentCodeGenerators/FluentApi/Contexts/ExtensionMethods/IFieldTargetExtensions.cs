using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IFieldTargetExtensions
{
    public static TContext WithField<TContext>(this TContext context, string name, INameInNamespaceToken typeInfo,
        Action<IFieldContext>? configure = null)
        where TContext : IFieldTarget<IContext>, IContext
    {
        context.AddUsing(typeInfo.Namespace);
        return context.WithField(name, typeInfo.Name.ToString(), configure);
    }

    public static TContext WithField<TContext>(this TContext context, string name, Type type,
        Action<IFieldContext>? configure = null)
        where TContext : IFieldTarget<IContext>, IContext
    {
        context.WithField(f =>
        {
            f.Named(name).OfType(type);
            configure?.Invoke(f);
        });
        return context;
    }

    public static TContext WithField<TContext>(this TContext context, string name, string type,
        Action<IFieldContext>? configure = null)
        where TContext : IFieldTarget<IContext>, IContext
    {
        context.WithField(f =>
        {
            f.Named(name);
            f.OfType(type);
            configure?.Invoke(f);
        });
        return context;
    }
}