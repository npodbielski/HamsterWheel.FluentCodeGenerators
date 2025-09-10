using System;
using HamsterWheel.FluentCodeGenerators.Chunks;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IBodyBuilderContext : IContext
{
    public IBodyBuilderContext Append(ICodeChunk chunk);
    IBodyBuilderContext InIndent(Action<IBodyBuilderContext> action);
}