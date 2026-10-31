using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IInterfaceTargetExtensions
{
    extension<TContext>(TContext context) where TContext : IInterfaceTarget<IContext>, IContext
    {
        public TContext ImplementsInterface(INameInNamespaceToken interfaceName,
            Action<IInterfaceImplementationContext>? configure = null)
        {
            context.ImplementsInterface(i =>
            {
                i.From(interfaceName);
                configure?.Invoke(i);
            });
            return context;
        }

        public TContext ImplementsInterface(Type interfaceType,
            Action<IInterfaceImplementationContext>? configure = null)
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

        public TContext ImplementsInterface(string interfaceName,
            Action<IInterfaceImplementationContext>? configure = null)
        {
            context.ImplementsInterface(i =>
            {
                i.From(interfaceName);
                configure?.Invoke(i);
            });
            return context;
        }
    }
}