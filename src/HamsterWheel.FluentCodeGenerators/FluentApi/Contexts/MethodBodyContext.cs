using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class MethodBodyContext(CodeBuilderContextBase previous, AppendableChunk chunks, CamelCaseName[] parameters)
    : CodeBuilderContextBase(previous), IMethodBodyContext
{
    public CamelCaseName[] ParametersNames => parameters;

    public IBodyBuilderContext Append(ICodeChunk chunk)
    {
        chunks.Append(chunk);
        return this;
    }

    public IBodyBuilderContext InIndent(Action<IBodyBuilderContext> action)
    {
        var indentedChunks = new AppendableChunk();
        var indent = new IndentedChunk(indentedChunks);
        chunks.Append(indent);
        action(new MethodBodyContext(this, indentedChunks, ParametersNames));
        return this;
    }

    public static IMethodBodyContext From<T>(T previous, MethodBodyChunks chunk)
        where T : CodeBuilderContextBase, IParametersDefinitionsBagContext
        => new MethodBodyContext(previous, chunk, previous.ParametersNames);
}

public static class MethodBodyContextExtensions
{
    extension(IMethodBodyContext context)
    {
        public IMethodBodyContext Append<TChunk>() where TChunk : ICodeChunk, new()
        {
            context.Append(new TChunk());
            return context;
        }

        public IMethodBodyContext AppendLine<TChunk>() where TChunk : ICodeChunk, new()
        {
            context.Append<TChunk>();
            context.Append(new EmptyLineChunk());
            return context;
        }

        public IMethodBodyContext AppendLine(ICodeChunk chunk)
        {
            context.Append(chunk);
            context.Append(new EmptyLineChunk());
            return context;
        }

        public IMethodBodyContext ConfigureUsing<T>()
            where T : IContextConfigurator<IMethodBodyContext>, new() => context.ConfigureUsing(new T());
    }
}