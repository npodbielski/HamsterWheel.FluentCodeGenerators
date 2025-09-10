using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IMethodTarget<out TContext> : IUsingsAppender
{
    TContext WithMethod(Action<IMethodContext> configure);
}