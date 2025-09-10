using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class PropertyContext(CodeBuilderContextBase previous, PropertyDefinitionChunk chunk)
    : CodeBuilderContextBase(previous), IPropertyContext
{
    public IPropertyContext Named(CamelCaseName name)
    {
        chunk.ReplaceName(name.ToPascalCase());
        return this;
    }

    public IPropertyContext OfType(Action<ITypeUsageContext> configure)
    {
        var typeNameChunk = TypeNameChunk.Object;
        chunk.SetType(typeNameChunk);
        var context = TypeUsageContext.From(this, typeNameChunk);
        configure(context);
        return this;
    }

    public IPropertyContext MakeGetOnly()
    {
        chunk.MakeGetOnly();
        return this;
    }

    public IPropertyContext MakeStatic()
    {
        chunk.MakeStatic();
        return this;
    }

    public IPropertyContext MakeOverride()
    {
        chunk.MakeOverride();
        return this;
    }

    public IPropertyContext MakeSealed()
    {
        chunk.MakeSealed();
        chunk.MakeOverride();
        return this;
    }

    public IPropertyContext MakeNullable()
    {
        chunk.MakeNullable();
        return this;
    }

    public IPropertyContext DisableNullabilityWarning()
    {
        if (((INullabilitySettings)this).Enabled)
        {
            chunk.DisableNullableWarning();
        }

        return this;
    }

    public IPropertyContext WithInitializer(Action<IExpressionBodyContext> configure)
    {
        var appendableChunk = new AppendableChunk();
        var body = new ExpressionBodyContext(this, appendableChunk, []);
        configure(body);
        chunk.WithInitializer(appendableChunk.Build());
        return this;
    }

    public IPropertyContext WithInitializer(ICodeChunk initChunk)
    {
        chunk.WithInitializer(initChunk.Build());
        return this;
    }

    public IPropertyContext MakeComputed()
    {
        chunk.MakeComputed();
        chunk.MakeGetOnly();
        chunk.GetExpressionBodyChunk.Append(new DefaultKeywordChunk());
        chunk.GetExpressionBodyChunk.AutoSemicolon = true;
        return this;
    }

    public IPropertyContext SetVisibility(MemberVisibility visibility)
    {
        chunk.SetVisibility(visibility);
        return this;
    }

    public IPropertyContext WithExpressionBody(Action<IExpressionBodyContext>? configure = null)
    {
        chunk.MakeComputed();
        chunk.GetExpressionBodyChunk = new ExpressionBodyChunks();
        var context = ExpressionBodyContext.From(this, chunk.GetExpressionBodyChunk);
        configure?.Invoke(context);
        return this;
    }

    public IPropertyContext WithSetExpressionBody(Action<IExpressionBodyContext>? configure = null)
    {
        chunk.MakeComputed();
        chunk.SetExpressionBodyChunk = new ExpressionBodyChunks();
        var context = ExpressionBodyContext.From(this, chunk.SetExpressionBodyChunk);
        configure?.Invoke(context);
        return this;
    }

    public static PropertyContext From(CodeBuilderContextBase previous, PropertyDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class PropertyContextExtensions
{
    public static IPropertyContext OfType<TProp>(this IPropertyContext context,
        Action<ITypeUsageContext>? configure = null) => context.OfType(typeof(TProp), configure);

    public static IPropertyContext OfType(this IPropertyContext context, Type type,
        Action<ITypeUsageContext>? configure = null) =>
        context.OfType(t =>
        {
            t.From(type);
            configure?.Invoke(t);
        });

    public static IPropertyContext WithNameOfInitializer(this IPropertyContext context, INameInNamespaceToken identifier)
    {
        context.AddUsing(identifier.Namespace);
        return context.WithInitializer(new NameOfExpressionChunk(identifier.Name));
    }

    public static IPropertyContext WithNewInitializer(this IPropertyContext context) =>
        context.WithInitializer<NewExpressionChunk>();

    public static IPropertyContext WithInitializer<TChunk>(this IPropertyContext context)
        where TChunk : ICodeChunk, new() => context.WithInitializer(new TChunk());

    public static IPropertyContext WithExpressionBody(this IPropertyContext context, string body) =>
        context.WithExpressionBody(c => c.Append(body));
}