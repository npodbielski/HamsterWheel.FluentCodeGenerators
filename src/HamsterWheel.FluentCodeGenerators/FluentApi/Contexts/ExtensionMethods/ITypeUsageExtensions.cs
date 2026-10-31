using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class ITypeUsageExtensions
{
    extension<TContext>(TContext context) where TContext : IContext, ITypeUsage<TContext>
    {
        public TContext WithGenericArgument(Type type,
            Action<ITypeUsageContext>? configure = null) =>
            context.WithGenericArgument(t =>
            {
                t.From(type);
                configure?.Invoke(t);
            });

        public TContext WithGenericArgument(INamedTypeSymbol argType,
            Action<ITypeUsageContext>? configure = null) =>
            context.WithGenericArgument(t =>
            {
                t.From(argType);
                configure?.Invoke(t);
            });

        public TContext WithGenericArgument(INameInNamespaceToken argType,
            Action<ITypeUsageContext>? configure = null) =>
            context.WithGenericArgument(t =>
            {
                t.From(argType);
                configure?.Invoke(t);
            });

        public TContext WithGenericArgument(PascalCaseName typeArg,
            Action<ITypeUsageContext>? configure = null) =>
            context.WithGenericArgument(t =>
            {
                t.From(typeArg);
                configure?.Invoke(t);
            });
    }

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

        var (name, @namespace, isArray) = type.ToNameInNamespace();
        var numberOfGenericArguments =
            type.IsGenericType ? type.GetGenericTypeDefinition().GetGenericArguments().Length : 0;

        context.From(name, @namespace, numberOfGenericArguments);
        if (isArray && context is ITypeUsageContext typeUsage)
        {
            typeUsage.MakeArray();
        }

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

    extension<TContext>(TContext context) where TContext : class, ITypeUsage<TContext>
    {
        public TContext From(PascalCaseName typeName) =>
            context.From(typeName);

        public TContext From(INameInNamespaceToken type) =>
            context.From(type.Name, type.Namespace);

        public TContext From(INamedTypeSymbol type) =>
            context.From(new PascalCaseName(type.Name), type.ContainingNamespace.ToString().ToNamespace());

        public TContext From(IExternalTypeInfo type) =>
            context.From(type.Name, type.Namespace, type.NumberOfGenericArgs);
    }
}