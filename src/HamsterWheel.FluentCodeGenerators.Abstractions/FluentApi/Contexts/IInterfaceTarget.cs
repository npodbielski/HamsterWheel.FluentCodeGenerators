using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IInterfaceTarget<out TContext> : IUsingsAppender
{
    TContext ImplementsInterface(Action<IInterfaceImplementationContext> configure);
}