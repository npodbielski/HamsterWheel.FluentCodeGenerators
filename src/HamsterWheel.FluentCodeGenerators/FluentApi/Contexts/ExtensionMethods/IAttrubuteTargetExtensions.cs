using System.CodeDom.Compiler;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

using static FluentApiSettings;

public static class IAttributeTargetExtensions
{
    public static TContext WithGeneratedCodeAttr<TContext>(this TContext context)
        where TContext : IAttributeTarget<IContext>, IContext =>
        (TContext)context.WithAttribute(a => a.From<GeneratedCodeAttribute>()
            .WithParameter(p => p.UseStringValue(GeneratorAssembly.FullName.Split(',')[0]))
            .WithParameter(p => p.UseStringValue(GeneratorAssembly.FullName.Split(',')[1])));

    public static TContext WithAttribute<TContext>(this TContext context, INameInNamespaceToken type,
        Action<ISingleAttributeContext>? configure = null)
        where TContext : IAttributeTarget<IContext>, IContext
    {
        context.WithAttribute(a =>
        {
            a.From(t => t.From(type));
            configure?.Invoke(a);
        });
        return context;
    }

    public static TContext WithAttribute<TContext>(this TContext context, Type type,
        Action<ISingleAttributeContext>? configure = null)
        where TContext : IAttributeTarget<IContext>, IContext
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

    public static TContext WithAttribute<TContext>(this TContext context, string name,
        Action<ISingleAttributeContext>? configure = null)
        where TContext : IAttributeTarget<IContext>, IContext
    {
        context.WithAttribute(a =>
        {
            a.From(t => t.From(name));
            configure?.Invoke(a);
        });
        return context;
    }
}