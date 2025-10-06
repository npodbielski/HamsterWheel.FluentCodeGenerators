using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IInheritingType<out TContext> : IUsingsAppender
{
    public TContext WithBase(Action<IImplementationsWithPrimaryCtorContext> configure);
}