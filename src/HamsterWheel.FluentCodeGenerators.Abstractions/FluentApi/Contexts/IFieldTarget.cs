using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IFieldTarget<out TContext>
{
    TContext WithField(Action<IFieldContext> configure);
}
