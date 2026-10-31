using System.Diagnostics.CodeAnalysis;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ParameterValueContext(CodeBuilderContextBase previous, ParameterValueChunk parameterValue)
    : CodeBuilderContextBase(previous), IParameterValueContext
{
    internal bool IsAttributeParameter { get; set; }

    public IParameterValueContext MakeNamed(string name)
    {
        parameterValue.AddName(name, IsAttributeParameter ? " = " : null);
        return this;
    }

    public IParameterValueContext UseStringValue(string value)
    {
        parameterValue.SetValue(new StringValueChunk(value));
        return this;
    }

    public IParameterValueContext UseConst(object value)
    {
        switch (value)
        {
            case true:
                UseConst("true");
                break;
            case false:
                UseConst("false");
                break;
            default:
                parameterValue.SetValue(value, Settings.DefaultCulture);
                break;
        }

        return this;
    }

    public static ParameterValueContext From(CodeBuilderContextBase previous, ParameterValueChunk chunk) =>
        new(previous, chunk);
}

public static class IParameterValueContextExtensions
{
    extension<TContext>(TContext context) where TContext : IParameterValueContext
    {
        public IParameterValueContext UseExpression([StringSyntax("C#")] BodyBuilderInterpolatedStringHandler builder)
        {
            foreach (var ns in builder.Namespaces)
            {
                context.AddUsing(ns);
            }

            context.UseConst(builder.GetFormattedText());
            return context;
        }
    }
}