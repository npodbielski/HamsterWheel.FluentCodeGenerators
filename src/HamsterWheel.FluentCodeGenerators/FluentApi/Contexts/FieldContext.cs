using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class FieldContext(CodeBuilderContextBase previous, FieldDefinitionChunk chunk)
    : CodeBuilderContextBase(previous), IFieldContext
{
    private Func<MemberDefinitionRecord, bool>? _whenPredicate;

    public IFieldContext Named(IName name)
    {
        chunk.ReplaceName(name.ToString());
        return this;
    }

    public IFieldContext MakeStatic()
    {
        chunk.MakeStatic();
        return this;
    }

    public IFieldContext MakeConst()
    {
        chunk.MakeConst();
        return this;
    }

    public IFieldContext MakeNullable()
    {
        chunk.MakeNullable();
        return this;
    }

    public IFieldContext DisableNullabilityWarning()
    {
        chunk.DisableNullableWarning();
        return this;
    }

    public IFieldContext WithInitializer(string value)
    {
        chunk.WithInitializer(value);
        return this;
    }

    public IFieldContext SetVisibility(MemberVisibility visibility)
    {
        chunk.SetVisibility(visibility);
        return this;
    }

    public IFieldContext OfType(Action<ITypeUsageContext> configure)
    {
        var typeNameChunk = TypeNameChunk.Object;
        chunk.SetType(typeNameChunk);
        var typeDef = TypeUsageContext.From(this, typeNameChunk);
        configure.Invoke(typeDef);
        return this;
    }

    public IFieldContext When(Func<MemberDefinitionRecord, bool> predicate)
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
        FieldDefinitionContext.BuildDefinition(FileChunks, chunk);

    public static FieldContext From(CodeBuilderContextBase previous, FieldDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class FieldContextExtensions
{
    extension(IFieldContext context)
    {
        public IFieldContext OfType<TField>() => context.OfType(t => t.From<TField>());

        public IFieldContext Named(string name) => context.Named(name.ToPascalCaseName().ToFieldName());
    }
}