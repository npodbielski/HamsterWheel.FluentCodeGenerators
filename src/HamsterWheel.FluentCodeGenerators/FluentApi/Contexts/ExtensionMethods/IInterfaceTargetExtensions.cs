using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IInterfaceTargetExtensions
{
    public static TContext WithInterface<TContext>(this TContext context, INameInNamespaceToken interfaceName,
        Action<IInterfaceImplementationContext>? configure = null) 
        where TContext : IInterfaceTarget<IContext>, IContext
    {
        context.ImplementsInterface(i =>
        {
            i.From(interfaceName);
            configure?.Invoke(i);
        });
        return context;
    }

    public static TContext WithInterface<TContext>(this TContext context, Type interfaceType,
        Action<IInterfaceImplementationContext>? configure = null) 
        where TContext : IInterfaceTarget<IContext>, IContext
    {
        if (!interfaceType.IsInterface)
        {
            throw new ArgumentException($"Type of {interfaceType.FullName} is not an interface type!");
        }

        context.ImplementsInterface(i =>
        {
            i.From(interfaceType);
            configure?.Invoke(i);
        });
        return context;
    }

    public static TContext WithInterface<TContext>(this TContext context, string interfaceName,
        Action<IInterfaceImplementationContext>? configure = null)
        where TContext : IInterfaceTarget<IContext>, IContext
    {
        context.ImplementsInterface(i =>
        {
            i.From(interfaceName);
            configure?.Invoke(i);
        });
        return context;
    }
}