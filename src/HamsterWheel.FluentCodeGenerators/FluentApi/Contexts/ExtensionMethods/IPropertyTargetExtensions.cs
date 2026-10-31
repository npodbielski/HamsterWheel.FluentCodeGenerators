using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IPropertyTargetExtensions
{
    extension<TContext>(TContext context) where TContext : IPropTarget<IContext>, IContext
    {
        public TContext WithCollectionProp(string propName, Type collectionType, Type itemType)
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

        public TContext WithProp(string name, INamedTypeSymbol type,
            Action<IPropertyContext>? configure = null)
        {
            context.WithProp(name, c =>
            {
                c.OfType(type);
                configure?.Invoke(c);
            });
            return context;
        }

        public TContext WithProp(string name, INameInNamespaceToken type,
            Action<IPropertyContext>? configure = null)
        {
            context.WithProp(name, c =>
            {
                c.OfType(type);
                configure?.Invoke(c);
            });
            return context;
        }

        public TContext WithProp(string name, Type type,
            Action<IPropertyContext>? configure = null)
        {
            context.WithProp(name, c =>
            {
                c.OfType(type);
                configure?.Invoke(c);
            });
            return context;
        }

        public TContext WithProp(string name, string type,
            Action<IPropertyContext>? configure = null)
        {
            context.WithProp(name, p =>
            {
                p.OfType(type);
                configure?.Invoke(p);
            });
            return context;
        }

        public TContext WithProp(string name,
            Action<IPropertyContext>? configure = null)
        {
            context.WithProp(p =>
            {
                p.Named(name);
                configure?.Invoke(p);
            });
            return context;
        }
    }
}