using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

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

    public IParameterDefinitionContext MakeRef()
    {
        paramDefChunk.MakeRef();
        return this;
    }

    public IParameterDefinitionContext MakeOut()
    {
        paramDefChunk.MakeOut();
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
    extension(IParameterDefinitionContext context)
    {
        public IParameterDefinitionContext Named(IName name)
        {
            context.Named(name.ToString());
            return context;
        }

        public IParameterDefinitionContext OfType<TParam>()
        {
            context.OfType(t => t.From<TParam>());
            return context;
        }

        public IParameterDefinitionContext OfType(INamedTypeSymbol typeSymbol)
        {
            context.OfType(t => t.From(typeSymbol));
            return context;
        }

        public IParameterDefinitionContext WithAttribute<TAttr>()
            where TAttr : Attribute => context.WithAttribute(a => a.From<TAttr>());
    }
}