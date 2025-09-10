using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class ITypeUsageExtensions
{
    public static TContext From<TContext>(this TContext context, Type type)
        where TContext : IContext, ITypeUsage<IContext>
    {
        context.AddUsing(type);
        if (type.IsNullable())
        {
            if (context is ITypeUsageContext typeUsageContext)
            {
                typeUsageContext.From((PascalCaseName)type.GenericTypeArguments.First().Name,
                        type.Namespace.ToNamespace())
                    .MakeNullable();
            }
            else
            {
                throw new IncorrectTypeUsageContextException<ITypeUsageContext, TContext>();
            }

            return context;
        }

        var (name, @namespace) = type.ToNameInNamespace();
        var numberOfGenericArguments =
            type.IsGenericType ? type.GetGenericTypeDefinition().GetGenericArguments().Length : 0;

        context.From(name, @namespace, numberOfGenericArguments);
        if (!type.IsGenericType || type.GenericTypeArguments.Length == 0)
        {
            return context;
        }

        foreach (var argument in type.GenericTypeArguments)
        {
            context.WithGenericArgument(a => a.From(argument));
        }

        return context;
    }

    public static TContext From<TContext>(this TContext context, PascalCaseName typeName)
        where TContext : class, ITypeUsage<TContext> =>
        context.From(typeName);

    public static TContext From<TContext>(this TContext context, INameInNamespaceToken type)
        where TContext : class, ITypeUsage<TContext> =>
        context.From(type.Name, type.Namespace);

    public static TContext From<TContext>(this TContext context, INamedTypeSymbol type)
        where TContext : class, ITypeUsage<TContext> =>
        context.From((PascalCaseName)type.Name, type.ContainingNamespace.ToString().ToNamespace());

    public static TContext From<TContext>(this TContext context, IExternalTypeInfo type)
        where TContext : class, ITypeUsage<TContext> =>
        context.From(type.Name, type.Namespace, type.NumberOfGenericArgs);

    public static TContext WithGenericArgument<TContext>(this TContext context, Type type,
        Action<ITypeUsageContext>? configure = null) where TContext : IContext, ITypeUsage<TContext> =>
        context.WithGenericArgument(t =>
        {
            t.From(type);
            configure?.Invoke(t);
        });

    public static TContext WithGenericArgument<TContext>(this TContext context, INameInNamespaceToken argType,
        Action<ITypeUsageContext>? configure = null) where TContext : IContext, ITypeUsage<TContext>
    {
        return context.WithGenericArgument(t =>
        {
            t.From(argType);
            configure?.Invoke(t);
        });
    }

    public static TContext WithGenericArgument<TContext>(this TContext context, PascalCaseName typeArg,
        Action<ITypeUsageContext>? configure = null) where TContext : IContext, ITypeUsage<TContext>
    {
        return context.WithGenericArgument(t =>
        {
            t.From(typeArg);
            configure?.Invoke(t);
        });
    }
}