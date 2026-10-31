using System.CodeDom.Compiler;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

using static FluentApiSettings;

public static class IAttributeTargetExtensions
{
    extension<TContext>(TContext context) where TContext : IAttributeTarget<IContext>, IContext
    {
        public TContext WithGeneratedCodeAttr()
        {
            var settings = (context as CodeBuilderContextBase)?.Settings ?? Snapshot();
            if (settings.AddGeneratedCodeAttribute)
            {
                return (TContext)context.WithAttribute(a => a.From<GeneratedCodeAttribute>()
                    .WithParameter(p => p.UseStringValue(settings.GeneratorAssembly.FullName.Split(',')[0]))
                    .WithParameter(p => p.UseStringValue(settings.GeneratorAssembly.FullName.Split(',')[1])));
            }

            return context;
        }

        public TContext WithAttribute(INameInNamespaceToken type,
            Action<ISingleAttributeContext>? configure = null)
        {
            context.WithAttribute(a =>
            {
                a.From(t => t.From(type));
                configure?.Invoke(a);
            });
            return context;
        }

        public TContext WithAttribute(Type type,
            Action<ISingleAttributeContext>? configure = null)
        {
            if (!type.IsAttribute())
            {
                throw new ArgumentException($"Parameter {nameof(type)} must inherit from {nameof(Attribute)} class!");
            }

            context.WithAttribute(a =>
            {
                a.From(t => t.From(type));
                configure?.Invoke(a);
            });
            return context;
        }

        public TContext WithAttribute(string name,
            Action<ISingleAttributeContext>? configure = null)
        {
            context.WithAttribute(a =>
            {
                a.From(t => t.From(name));
                configure?.Invoke(a);
            });
            return context;
        }
    }
}