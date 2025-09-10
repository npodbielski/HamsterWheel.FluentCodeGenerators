using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IBodyBuilderContextExtensions
{
    public static TContext AppendTypeUsage<TContext>(this TContext context, Action<ITypeUsageContext> configure)
        where TContext : IBodyBuilderContext
    {
        var typeNameChunk = TypeNameChunk.Object;
        var contextBase = context as CodeBuilderContextBase ?? throw new IncorrectTypeUsageContextException<CodeBuilderContextBase, TContext>();
        var typeContext = TypeUsageContext.From(contextBase, typeNameChunk);
        configure(typeContext);
        context.Append(typeNameChunk);
        return context;
    }

    public static TContext Append<TContext>(this TContext context,
        [StringSyntax("C#")] string statement)
        where TContext : IBodyBuilderContext
    {
        context.Append(new PlainValueChunk(statement));
        return context;
    }

    public static TContext Append<TContext>(this TContext context,
        [StringSyntax("C#")] BodyBuilderInterpolatedStringHandler builder)
        where TContext : IBodyBuilderContext
    {
        foreach (var ns in builder.Namespaces)
        {
            context.AddUsing(ns);
        }

        context.Append(builder.GetFormattedText());
        return context;
    }

    public static TContext AppendLine<TContext>(this TContext context,
        [StringSyntax("C#")] BodyBuilderInterpolatedStringHandler builder)
        where TContext : IBodyBuilderContext
    {
        context.Append(builder);
        context.Append(new EmptyLineChunk());
        return context;
    }

    public static TContext AppendLine<TContext>(this TContext context, [StringSyntax("C#")] string? statement = null)
        where TContext : IBodyBuilderContext
    {
        context.Append(statement ?? "");
        context.Append(new EmptyLineChunk());
        return context;
    }

    public static TContext AppendComment<TContext>(this TContext context, [StringSyntax("comment")] string comment)
        where TContext : IBodyBuilderContext
    {
        context.Append("//");
        context.Append(comment);
        context.Append(new EmptyLineChunk());
        return context;
    }
}