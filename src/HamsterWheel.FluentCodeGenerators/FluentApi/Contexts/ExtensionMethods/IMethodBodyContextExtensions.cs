using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IMethodBodyContextExtensions
{
    public static TContext AppendReturn<TContext>(this TContext context, [StringSyntax("C#")] string statement)
        where TContext : IMethodBodyContext, IBodyBuilderContext
    {
        context.Append(new ReturnKeywordChunk());
        ((IBodyBuilderContext)context).Append(statement);
        if (!statement.EndsWith(";"))
        {
            ((IBodyBuilderContext)context).Append(";");
        }

        context.Append(new EmptyLineChunk());
        return context;
    }
}