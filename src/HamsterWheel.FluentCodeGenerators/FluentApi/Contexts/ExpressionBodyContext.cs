using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ExpressionBodyContext(CodeBuilderContextBase previous, AppendableChunk chunks, CamelCaseName[] parameters)
    : CodeBuilderContextBase(previous), IExpressionBodyContext
{
    public CamelCaseName[] ParameterNames { get; } = parameters;

    public IBodyBuilderContext Append([StringSyntax("C#")] string statement)
    {
        var chunk = new PlainValueChunk(statement ?? "");
        chunks.Append(chunk);
        return this;
    }

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
        action(new ExpressionBodyContext(this, indentedChunks, ParameterNames));
        return this;
    }

    public static ExpressionBodyContext From(CodeBuilderContextBase previous, ExpressionBodyChunks chunks, CamelCaseName[]? parameters = null)
    {
        chunks.AutoSemicolon = true;
        return new ExpressionBodyContext(previous, chunks, parameters ?? []);
    }
}

public static class ExpressionBodyContextExtensions
{
    public static IExpressionBodyContext ConfigureUsing<T>(this IExpressionBodyContext context)
        where T : IContextConfigurator<IExpressionBodyContext>, new() => context.ConfigureUsing(new T());
}