using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Body;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class PropertyContext(CodeBuilderContextBase previous, PropertyDefinitionChunk chunk)
    : CodeBuilderContextBase(previous), IPropertyContext
{
    private Func<MemberDefinitionRecord, bool>? _whenPredicate;

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

    public IPropertyContext SetSetterVisibility(MemberVisibility visibility)
    {
        chunk.SetSetterVisibility(visibility);
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

    public IPropertyContext When(Func<MemberDefinitionRecord, bool> predicate)
    {
        _whenPredicate = predicate;
        return this;
    }

    public void SuppressWhenFalse()
    {
        if (_whenPredicate is null)
            return;

        var definition = BuildDefinition();
        if (!_whenPredicate(definition))
            chunk.SuppressEmit();
    }

    private MemberDefinitionRecord BuildDefinition() =>
        PropertyDefinitionContext.BuildDefinition(FileChunks, chunk);

    public static PropertyContext From(CodeBuilderContextBase previous, PropertyDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class PropertyContextExtensions
{
    extension(IPropertyContext context)
    {
        public IPropertyContext OfType<TProp>(Action<ITypeUsageContext>? configure = null) =>
            context.OfType(typeof(TProp), configure);

        public IPropertyContext OfType(Type type,
            Action<ITypeUsageContext>? configure = null) =>
            context.OfType(t =>
            {
                t.From(type);
                configure?.Invoke(t);
            });

        public IPropertyContext WithNameOfInitializer(INameInNamespaceToken identifier)
        {
            context.AddUsing(identifier.Namespace);
            return context.WithInitializer(new NameOfExpressionChunk(identifier.Name));
        }

        public IPropertyContext WithNewInitializer() =>
            context.WithInitializer<NewExpressionChunk>();

        public IPropertyContext WithInitializer<TChunk>()
            where TChunk : ICodeChunk, new() => context.WithInitializer(new TChunk());

        public IPropertyContext WithExpressionBody(string body) =>
            context.WithExpressionBody(c => c.Append(body));
    }
}