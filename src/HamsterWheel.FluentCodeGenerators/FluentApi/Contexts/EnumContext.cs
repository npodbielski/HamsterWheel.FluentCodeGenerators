using System.Runtime.Serialization;
using HamsterWheel.FluentCodeGenerators.Chunks.Enums;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class EnumContext(CodeBuilderContextBase previous, EnumChunk chunk)
    : CodeBuilderContextBase(previous), IEnumContext, IName
{
    public PascalCaseName Name => chunk.Name;

    public string NameAsString => Name;

    public IEnumContext Named(string name)
    {
        chunk.ReplaceName(name);
        return this;
    }

    public IEnumContext WithAttribute(Action<ISingleAttributeContext> configure)
    {
        var currentAttrChunk = AttributeDefinitionChunk.From("void");
        chunk.AddAttribute(currentAttrChunk);
        var context = SingleAttributeContext.From(this, currentAttrChunk);
        configure(context);
        return this;
    }

    public IEnumContext WithValues(IEnumerable<string> values)
    {
        foreach (var value in values)
        {
            WithValue(value);
        }

        return this;
    }

    public IEnumContext WithValue(string value, int? number = null)
    {
        var enumValueChunk = new EnumValueChunk(value.ToPascalCase(), number);
        if (value.Contains("-"))
        {
            UsingsAppender.AddUsing<EnumMemberAttribute>();
            enumValueChunk = new EnumValueChunk(PascalCaseName.FromChunks(value.Split('-').Select(n => n.ToPascalCaseName())));
            var valueAttrChunk = AttributeDefinitionChunk.Empty();
            var attrContext = SingleAttributeContext.From(this, valueAttrChunk);
            attrContext.From<EnumMemberAttribute>()
                .WithParameter(p =>
                {
                    p.UseStringValue(value)
                        .MakeNamed(nameof(EnumMemberAttribute.Value));
                });
            enumValueChunk.AddAttribute(valueAttrChunk);
        }

        chunk.AddValue(enumValueChunk);
        return this;
    }

    public IEnumContext SetVisibility(MemberVisibility visibility)
    {
        chunk.SetVisibility(visibility);
        return this;
    }

    public static EnumContext From(CodeBuilderContextBase previous, EnumChunk callChunk) =>
        new(previous, callChunk);
}

public static class EnumDefinitionContextExtensions
{
    public static IEnumContext WithAttribute<TAttr>(this IEnumContext context)
        where TAttr : Attribute =>
        context.WithAttribute(a => a.From<TAttr>());
}