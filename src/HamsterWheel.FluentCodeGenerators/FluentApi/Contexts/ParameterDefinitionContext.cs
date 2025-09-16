using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ParameterDefinitionContext(CodeBuilderContextBase previous, ParameterDefinitionChunk paramDefChunk)
    : CodeBuilderContextBase(previous), IParameterDefinitionContext
{
    public IParameterDefinitionContext WithAttribute(Action<ISingleAttributeContext> configure)
    {
        var currentAttrChunk = AttributeDefinitionChunk.Empty();
        var context = SingleAttributeContext.From(this, currentAttrChunk);
        paramDefChunk.AddAttribute(currentAttrChunk);
        configure(context);
        return this;
    }

    public IParameterDefinitionContext Named(string name)
    {
        paramDefChunk.SetName(name);
        return this;
    }

    public IParameterDefinitionContext MakeNullable()
    {
        paramDefChunk.MakeNullable();
        return this;
    }

    public IParameterDefinitionContext OfType(Action<ITypeUsageContext> configure)
    {
        var typeNameChunk = new TypeNameChunk("void");
        paramDefChunk.SetType(typeNameChunk);
        var context = TypeUsageContext.From(this, typeNameChunk);
        configure(context);
        return this;
    }

    public void PushToEnd() => paramDefChunk.Order = 9999;

    public static ParameterDefinitionContext From(CodeBuilderContextBase previous, ParameterDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class ParameterDefinitionContextExtensions
{
    public static IParameterDefinitionContext Named(this IParameterDefinitionContext context, IName name)
    {
        context.Named(name.ToString());
        return context;
    }

    public static IParameterDefinitionContext OfType<TParam>(this IParameterDefinitionContext context)
    {
        context.OfType(t => t.From<TParam>());
        return context;
    }

    public static IParameterDefinitionContext WithAttribute<TAttr>(this IParameterDefinitionContext context)
        where TAttr : Attribute => context.WithAttribute(a => a.From<TAttr>());
}