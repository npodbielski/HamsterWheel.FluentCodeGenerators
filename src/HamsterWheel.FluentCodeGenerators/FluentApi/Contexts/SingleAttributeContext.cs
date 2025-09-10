using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class SingleAttributeContext(CodeBuilderContextBase previous, AttributeDefinitionChunk attributeChunk)
    : CodeBuilderContextBase(previous), ISingleAttributeContext
{
    public ISingleAttributeContext From(Action<ITypeUsageContext> configure)
    {
        var chunk = TypeNameChunk.From(typeof(Attribute), true);
        var context = TypeUsageContext.From(this, chunk);
        configure(context);
        attributeChunk.ReplaceType(chunk);
        return this;
    }

    public ISingleAttributeContext WithParameter(Action<IParameterValueContext> configure)
    {
        var parameterValueChunk = ParameterValueChunk.From("");
        attributeChunk.AddParameter(parameterValueChunk);
        var context = new ParameterValueContext(this, parameterValueChunk)
        {
            IsAttributeParameter = true
        };
        configure(context);
        return this;
    }

    public static SingleAttributeContext From(CodeBuilderContextBase previous, AttributeDefinitionChunk currentAttr) =>
        new(previous, currentAttr);
}

public static class SingleAttributeContextExtensions
{
    public static ISingleAttributeContext From<TAttr>(this ISingleAttributeContext context) where TAttr : Attribute
    {
        context.From(a => a.From(typeof(TAttr)));
        return context;
    }

    public static ISingleAttributeContext From(this ISingleAttributeContext context, string typeName,
        string? namespaceName = null)
    {
        context.From(a => a.From((PascalCaseName)typeName, namespaceName?.ToNamespace()));
        return context;
    }

    public static ISingleAttributeContext WithParameter(this ISingleAttributeContext context, string value,
        Action<IParameterValueContext>? configure = null)
    {
        context.WithParameter(p =>
        {
            p.UseExpression(value);
            configure?.Invoke(p);
        });
        return context;
    }
}