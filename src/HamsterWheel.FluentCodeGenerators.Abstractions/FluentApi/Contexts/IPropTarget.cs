using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IPropTarget<out TContext> : IUsingsAppender
{
    TContext WithProp(Action<IPropertyContext> configure);
}