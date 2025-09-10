using System;
using System.Collections.Generic;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IPrimaryConstructorDefinitionContext : IContext
{
    Dictionary<CamelCaseName, INamedChunk> Parameters { get; }
    IPrimaryConstructorDefinitionContext WithParameter(Action<IParameterDefinitionContext> configure);
}