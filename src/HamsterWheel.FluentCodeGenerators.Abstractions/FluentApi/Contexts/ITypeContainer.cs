using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface ITypeContainer<out TContext> : IUsingsAppender
{
    TContext WithClass(Action<IClassContext> configure);
    TContext WithEnum(Action<IEnumContext> configurator);
}