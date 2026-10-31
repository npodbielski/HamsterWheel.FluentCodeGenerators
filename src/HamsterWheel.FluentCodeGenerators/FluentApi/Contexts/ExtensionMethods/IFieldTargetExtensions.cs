using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IFieldTargetExtensions
{
    extension<TContext>(TContext context) where TContext : IFieldTarget<IContext>, IContext
    {
        public TContext WithField(string name, INameInNamespaceToken typeInfo,
            Action<IFieldContext>? configure = null)
        {
            context.AddUsing(typeInfo.Namespace);
            return context.WithField(name, typeInfo.Name.ToString(), configure);
        }

        public TContext WithField(string name, Type type,
            Action<IFieldContext>? configure = null)
        {
            context.WithField(f =>
            {
                f.Named(name).OfType(type);
                configure?.Invoke(f);
            });
            return context;
        }

        public TContext WithField(string name, string type,
            Action<IFieldContext>? configure = null)
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
}