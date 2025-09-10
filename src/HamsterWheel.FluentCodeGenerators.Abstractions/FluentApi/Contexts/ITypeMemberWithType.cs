using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ITypeMemberWithType<out TContext> : IUsingsAppender
{
    TContext OfType(Action<ITypeUsageContext> configure);
}