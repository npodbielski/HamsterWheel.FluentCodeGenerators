using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class ClassContext(CodeBuilderContextBase previous, ClassDefinitionChunk classChunk)
    : CodeBuilderContextBase(previous), IClassContext, IName
{
    public CamelCaseName[] PrimaryCtorParametersNames =>
        classChunk.ClassName.PrimaryConstructor?.Parameters.Keys.Select(n => n).ToArray() ?? [];

    public PascalCaseName Name => classChunk.Name;

    public string NameAsString => Name;

    public IClassContext Named(string name)
    {
        classChunk.ReplaceName(name);
        return this;
    }

    public IClassContext WithComment(Action<ISummaryCommentContext> configure)
    {
        var chunk = new SummaryCommentChunk();
        classChunk.AddComment(chunk);
        configure.Invoke(SummaryCommentContext.From(this, chunk));

        return this;
    }

    public IClassContext MakePartial()
    {
        classChunk.MakePartial();
        return this;
    }

    public IClassContext MakeStatic()
    {
        classChunk.MakeStatic();
        return this;
    }

    public IClassContext MakeAbstract()
    {
        classChunk.MakeAbstract();
        return this;
    }

    public IClassContext MakeSealed()
    {
        //TODO: disallow making class sealed and static at the same time by returning different types I.e. ISealedClassContext that won't be having MakeStatic method
        classChunk.MakeSealed();
        return this;
    }

    public IClassContext WithPrimaryCtor(Action<IPrimaryConstructorDefinitionContext> configure)
    {
        var chunk = new PrimaryConstructorDefinitionChunk([]);
        classChunk.AddPrimaryCtor(chunk);
        var context = PrimaryConstructorDefinitionContext.From(this, chunk);
        configure(context);
        return this;
    }

    public IClassContext WithBase(Action<IImplementationsWithPrimaryCtorContext> configure)
    {
        var baseTypeContext = ImplementationsWithPrimaryCtorContext<ClassContext>.From(this, classChunk.Implementations);
        configure(baseTypeContext);
        return this;
    }

    public IClassContext WithCtor(Action<IConstructorContext> configure)
    {
        var chunk = new CtorDefinitionChunk();
        classChunk.AddCtor(chunk);
        configure(ConstructorContext.From(this, chunk));
        return this;
    }

    public IClassContext WithMethod(Action<IMethodContext> configure)
    {
        var chunk = new MethodDefinitionChunk("MyMethod");
        classChunk.AddMethod(chunk);
        configure(MethodContext.From(this, chunk));
        return this;
    }

    public IClassContext ImplementsInterface(Action<IInterfaceImplementationContext> configure)
    {
        var implementations = classChunk.Implementations;
        var typeChunk = TypeNameChunk.Object;
        implementations.AddInterface(typeChunk);
        var interfaceContext = new InterfaceImplementationContext(this, typeChunk);
        configure.Invoke(interfaceContext);
        return this;
    }

    public IClassContext WithAttribute(Action<ISingleAttributeContext> configure)
    {
        var currentAttrChunk = AttributeDefinitionChunk.Empty();
        var context = SingleAttributeContext.From(this, currentAttrChunk);
        classChunk.AddAttribute(currentAttrChunk);
        configure(context);
        return this;
    }

    public IClassContext WithCode(StringBuilder code)
    {
        classChunk.Append(code);
        return this;
    }

    public IClassContext WithProp(Action<IPropertyContext>? configure = null)
    {
        var newPropChunk = PropertyDefinitionChunk.From("Property");
        classChunk.AddProperty(newPropChunk);
        var propertyContext = PropertyContext.From(this, newPropChunk);
        configure?.Invoke(propertyContext);
        return this;
    }

    public IClassContext WithField(Action<IFieldContext>? configure = null)
    {
        var newChunk = FieldDefinitionChunk.From("_field1");
        classChunk.AddField(newChunk);
        configure?.Invoke(new FieldContext(this, newChunk));
        return this;
    }

    public IClassContext SetVisibility(MemberVisibility visibility)
    {
        classChunk.SetVisibility(visibility);
        return this;
    }
}

public static class ClassContextExtensions
{
    public static IClassContext WithAttribute<TAttr>(this IClassContext context) where TAttr : Attribute =>
        context.WithAttribute(a => a.From<TAttr>());

    public static IClassContext ImplementsInterface<TInterface>(this IClassContext context,
        Action<IInterfaceImplementationContext>? configure = null)
        where TInterface : class
    {
        context.ImplementsInterface(i =>
        {
            i.From<TInterface>();
            configure?.Invoke(i);
        });
        return context;
    }

    public static IClassContext WithProp<TProp>(this IClassContext context, string name,
        Action<IPropertyContext>? configure = null)
    {
        context.WithProp(name, typeof(TProp), c => configure?.Invoke(c));
        return context;
    }

    public static IClassContext WithMethod(this IClassContext context, string name,
        Action<IMethodContext>? configure = null)
    {
        context.WithMethod(c =>
        {
            c.Named(name);
            configure?.Invoke(c);
        });
        return context;
    }

    public static IClassContext WithField<TProp>(this IClassContext context, string name,
        Action<IFieldContext>? configure = null) =>
        context.WithField(name, typeof(TProp), c => configure?.Invoke(c));

    public static IClassContext WithCode(this IClassContext context, string code) =>
        context.WithCode(new StringBuilder(code));

    public static IClassContext ConfigureUsing<T>(this IClassContext context)
        where T : IContextConfigurator<IClassContext>, new() => context.ConfigureUsing(new T());
}