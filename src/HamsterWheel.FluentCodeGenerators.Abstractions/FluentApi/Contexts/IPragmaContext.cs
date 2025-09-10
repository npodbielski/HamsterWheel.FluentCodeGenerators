using System;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IPragmaContext : IContext
{
    IContext Nullability(Action<INullabilityPragmaContext> configure);
}