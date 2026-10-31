using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IInheritingTypeExtensions
{
    extension<TContext>(TContext context) where TContext : IInheritingType<IContext>, IContext
    {
        public TContext WithBase(INamedTypeSymbol name,
            Action<IImplementationsWithPrimaryCtorContext>? configure = null)
        {
            context.WithBase(b =>
            {
                b.From(name);
                configure?.Invoke(b);
            });
            return context;
        }

        public TContext WithBase(INameInNamespaceToken name,
            Action<IImplementationsWithPrimaryCtorContext>? configure = null)
        {
            context.WithBase(b =>
            {
                b.From(name);
                configure?.Invoke(b);
            });
            return context;
        }

        public TContext WithBase(string name, string @namespace,
            Action<IImplementationsWithPrimaryCtorContext>? configure = null)
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
}