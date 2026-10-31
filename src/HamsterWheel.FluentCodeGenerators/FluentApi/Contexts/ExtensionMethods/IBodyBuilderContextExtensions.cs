using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IBodyBuilderContextExtensions
{
    extension<TContext>(TContext context) where TContext : IBodyBuilderContext
    {
        public TContext AppendTypeUsage(Action<ITypeUsageContext> configure)
        {
            var typeNameChunk = TypeNameChunk.Object;
            var contextBase = context as CodeBuilderContextBase ?? throw new IncorrectTypeUsageContextException<CodeBuilderContextBase, TContext>();
            var typeContext = TypeUsageContext.From(contextBase, typeNameChunk);
            configure(typeContext);
            context.Append(typeNameChunk);
            return context;
        }

        public TContext Append([StringSyntax("C#")] string statement)
        {
            context.Append(new PlainValueChunk(statement));
            return context;
        }

        public TContext Append([StringSyntax("C#")] BodyBuilderInterpolatedStringHandler builder)
        {
            foreach (var ns in builder.Namespaces)
            {
                context.AddUsing(ns);
            }

            context.Append(builder.GetFormattedText());
            return context;
        }

        public TContext AppendLine([StringSyntax("C#")] BodyBuilderInterpolatedStringHandler builder)
        {
            context.Append(builder);
            context.Append(new EmptyLineChunk());
            return context;
        }

        public TContext AppendLine([StringSyntax("C#")] string? statement = null)
        {
            context.Append(statement ?? "");
            context.Append(new EmptyLineChunk());
            return context;
        }

        public TContext AppendComment([StringSyntax("comment")] string comment)
        {
            context.Append("//");
            context.Append(comment);
            context.Append(new EmptyLineChunk());
            return context;
        }
    }
}