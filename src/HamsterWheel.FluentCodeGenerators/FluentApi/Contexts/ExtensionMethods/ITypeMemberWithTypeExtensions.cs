using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class ITypeMemberWithTypeExtensions
{
    extension<TContext>(TContext context) where TContext : ITypeMemberWithType<TContext>, IContext
    {
        public TContext OfType(INamedTypeSymbol typeToken, Action<ITypeUsageContext>? configure = null)
        {
            context.OfType(t =>
            {
                t.From(typeToken);
                configure?.Invoke(t);
            });
            return context;
        }

        public TContext OfType(INameInNamespaceToken typeToken,
            Action<ITypeUsageContext>? configure = null)
        {
            context.OfType(t =>
            {
                t.From(typeToken);
                configure?.Invoke(t);
            });
            return context;
        }

        public TContext OfType(Type type,
            Action<ITypeUsageContext>? configure = null)
        {
            context.OfType(t =>
            {
                t.From(type);
                configure?.Invoke(t);
            });
            return context;
        }

        public TContext OfType(string type,
            Action<ITypeUsageContext>? configure = null)
        {
            context.OfType(t =>
            {
                t.From(new PascalCaseName(type));
                configure?.Invoke(t);
            });
            return context;
        }
    }
}