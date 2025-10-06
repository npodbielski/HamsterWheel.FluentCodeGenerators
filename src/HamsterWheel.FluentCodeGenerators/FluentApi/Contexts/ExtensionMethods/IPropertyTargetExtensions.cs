using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IPropertyTargetExtensions
{
    public static TContext WithCollectionProp<TContext>(
        this TContext context, string propName, Type collectionType, Type itemType)
        where TContext : IPropTarget<IContext>, IContext
    {
        return context.WithProp(propName, p =>
        {
            p.OfType(t => t.From(collectionType).WithGenericArgument(itemType));
            if (((INullabilitySettings)context).Enabled)
            {
                p.DisableNullabilityWarning();
            }
        });
    }

    public static TContext WithProp<TContext>(this TContext context, string name, INameInNamespaceToken type,
        Action<IPropertyContext>? configure = null)
        where TContext : IPropTarget<IContext>, IContext
    {
        context.WithProp(name, c =>
        {
            c.OfType(type);
            configure?.Invoke(c);
        });
        return context;
    }

    public static TContext WithProp<TContext>(this TContext context, string name, Type type,
        Action<IPropertyContext>? configure = null)
        where TContext : IPropTarget<IContext>, IContext
    {
        context.WithProp(name, c =>
        {
            c.OfType(type);
            configure?.Invoke(c);
        });
        return context;
    }

    public static TContext WithProp<TContext>(this TContext context, string name, string type,
        Action<IPropertyContext>? configure = null)
        where TContext : IPropTarget<IContext>, IContext
    {
        context.WithProp(name, p =>
        {
            p.OfType(type);
            configure?.Invoke(p);
        });
        return context;
    }

    public static TContext WithProp<TContext>(this TContext context, string name,
        Action<IPropertyContext>? configure = null)
        where TContext : IPropTarget<IContext>, IContext
    {
        context.WithProp(p =>
        {
            p.Named(name);
            configure?.Invoke(p);
        });
        return context;
    }
}