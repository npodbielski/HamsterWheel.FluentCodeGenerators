using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class FieldContext(CodeBuilderContextBase previous, FieldDefinitionChunk chunk)
    : CodeBuilderContextBase(previous), IFieldContext
{
    public IFieldContext Named(string name)
    {
        chunk.ReplaceName(name.ToPascalCaseName().ToFieldName());
        return this;
    }

    public IFieldContext MakeStatic()
    {
        chunk.MakeStatic();
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

    public IFieldContext OfType(Action<ITypeUsageContext> configure)
    {
        var typeNameChunk = TypeNameChunk.Object;
        chunk.SetType(typeNameChunk);
        var typeDef = TypeUsageContext.From(this, typeNameChunk);
        configure.Invoke(typeDef);
        return this;
    }

    public static FieldContext From(CodeBuilderContextBase previous, FieldDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class FieldContextExtensions
{
    public static IFieldContext OfType<TField>(this IFieldContext context) => context.OfType(t => t.From<TField>());
}