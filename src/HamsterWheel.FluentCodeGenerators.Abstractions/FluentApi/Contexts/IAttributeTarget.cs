using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IAttributeTarget<out TContext> : IUsingsAppender
{
    public TContext WithAttribute(Action<ISingleAttributeContext> configure);
}