using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IMemberWithTheBody<out TContext>
{
    TContext WithBody(Action<IMethodBodyContext>? configure = null);
    TContext WithExpressionBody(Action<IExpressionBodyContext>? configure = null);
}