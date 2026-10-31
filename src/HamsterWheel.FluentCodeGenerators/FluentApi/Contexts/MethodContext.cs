using HamsterWheel.FluentCodeGenerators.Chunks.Member;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Configurators;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;
using HamsterWheel.FluentCodeGenerators.Members;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public class MethodContext(CodeBuilderContextBase previous, MethodDefinitionChunk methodChunk)
    : CodeBuilderContextBase(previous), IMethodContext
{
    private Func<MethodDefinitionRecord, bool>? _whenPredicate;

    internal MethodDefinitionChunk Chunk => methodChunk;
    internal PascalCaseName? ClassName => previous is ClassContext classContext ? classContext.Name : null;

    public CamelCaseName[] ParametersNames => methodChunk.ParameterNames;

    public IMethodContext Named(string name)
    {
        methodChunk.ReplaceName(name);
        return this;
    }

    public IMethodContext WithBody(Action<IMethodBodyContext>? configure = null)
    {
        var context = MethodBodyContext.From(this, methodChunk.BodyChunks);
        configure?.Invoke(context);
        return this;
    }

    public IMethodContext WithExpressionBody(Action<IExpressionBodyContext>? configure = null)
    {
        methodChunk.AsExpressionBody();
        var context = ExpressionBodyContext.From(this, methodChunk.ExpressionBodyChunk, methodChunk.ParameterNames);
        configure?.Invoke(context);
        return this;
    }

    public IMethodContext WithParameter(Action<IParameterDefinitionContext> configure)
    {
        var paramDefChunk = new ParameterDefinitionChunk();
        configure(new ParameterDefinitionContext(this, paramDefChunk));
        paramDefChunk.SetNameBasedOnType();

        methodChunk.AddParameter(paramDefChunk);
        return this;
    }

    public IMethodContext MakeStatic()
    {
        methodChunk.MakeStatic();
        return this;
    }

    public IMethodContext SetVisibility(MemberVisibility visibility)
    {
        methodChunk.SetVisibility(visibility);
        return this;
    }

    public IMethodContext WithReturnType(Action<ITypeUsageContext> configure)
    {
        var typeChunk = new TypeNameChunk("void");
        methodChunk.SetReturnType(typeChunk);
        var context = TypeUsageContext.From(this, typeChunk);
        configure(context);
        return this;
    }

    public IMethodContext MakeOverride()
    {
        methodChunk.MakeOverride();
        return this;
    }

    public IMethodContext MakeSealed()
    {
        methodChunk.MakeSealed();
        return this;
    }

    public IMethodContext MakeAsync()
    {
        UsingsAppender.AddUsing<Task>();
        methodChunk.MakeAsync();
        return this;
    }

    public IMethodContext MakeAsyncValueTask()
    {
        UsingsAppender.AddUsing<ValueTask>();
        methodChunk.MakeAsyncValueTask();
        return this;
    }

    public IMethodContext MakeVirtual()
    {
        methodChunk.MakeVirtual();
        return this;
    }

    public IMethodContext MakePartial()
    {
        methodChunk.MakePartial();
        return this;
    }

    public IMethodContext MakeAbstract()
    {
        methodChunk.MakeAbstract();
        return this;
    }

    public IMethodContext WithAttribute(Action<ISingleAttributeContext> configure)
    {
        var currentAttrChunk = AttributeDefinitionChunk.Empty();
        var context = SingleAttributeContext.From(this, currentAttrChunk);
        methodChunk.AddAttribute(currentAttrChunk);
        configure(context);
        return this;
    }

    public IMethodContext WithComment(Action<ISummaryCommentContext> configure)
    {
        var chunk = new SummaryCommentChunk();
        methodChunk.AddComment(chunk);
        configure.Invoke(SummaryCommentContext.From(this, chunk));

        return this;
    }

    public IMethodContext When(Func<MethodDefinitionRecord, bool> predicate)
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
            methodChunk.SuppressEmit();
    }

    private MethodDefinitionRecord BuildDefinition() =>
        MethodDefinitionContext.BuildDefinition(FileChunks, methodChunk, ClassName);

    public static MethodContext From(CodeBuilderContextBase previous, MethodDefinitionChunk chunk) =>
        new(previous, chunk);
}

public static class MethodContextExtensions
{
    extension(IMethodContext context)
    {
        public IMethodContext WithBody<TConfigurator>()
            where TConfigurator : IContextConfigurator<IMethodBodyContext>, new()
        {
            context.WithBody(new TConfigurator());
            return context;
        }

        public IMethodContext WithBody(IContextConfigurator<IMethodBodyContext> configurator)
        {
            context.WithBody(configurator.Configure);
            return context;
        }

        public IMethodContext WithCancellation() =>
            context.WithParameter<CancellationToken>((PascalCaseName)nameof(CancellationToken).ToCamelCase(),
                p => p.PushToEnd());

        public IMethodContext WithParameter<TParam>(IName name,
            Action<IParameterDefinitionContext>? configure = null)
        {
            context.AddUsing<TParam>();
            var result = context.WithParameter(name, typeof(TParam).Name, configure);
            SetFullTypeOnLastParameter(GetMethodContext(context).Chunk,
                NameInNamespace.From(typeof(TParam).Name, typeof(TParam).Namespace!));
            return result;
        }

        public IMethodContext WithParameter(IName name, INameInNamespaceToken type,
            Action<IParameterDefinitionContext>? configure = null)
        {
            context.AddUsing(type);
            return context.WithParameter(name, type.Name, configure);
        }

        public IMethodContext WithParameter(IName name, Type type,
            Action<IParameterDefinitionContext>? configure = null)
        {
            context.AddUsing(type);
            var result = context.WithParameter(name, type.Name, configure);
            SetFullTypeOnLastParameter(GetMethodContext(context).Chunk,
                NameInNamespace.From(type.Name, type.Namespace!));
            return result;
        }

        public IMethodContext WithParameter(IName name, string? type = null,
            Action<IParameterDefinitionContext>? configure = null)
        {
            return context.WithParameter(p =>
            {
                p.Named(name);
                if (type != null)
                {
                    p.OfType(type);
                }

                configure?.Invoke(p);
            });
        }

        private static void SetFullTypeOnLastParameter(MethodDefinitionChunk chunk, NameInNamespace fullType)
        {
            var parameters = chunk.ParametersList;
            if (parameters.Count > 0)
            {
                parameters[^1].SetFullType(fullType);
            }
        }

        private static MethodContext GetMethodContext(IMethodContext ctx) =>
            ctx as MethodContext ?? throw new InvalidOperationException();

        public IMethodContext WithReturnType<TReturn>(Action<ITypeUsageContext>? configure = null)
        {
            context.WithReturnType(typeof(TReturn), configure);
            return context;
        }

        public IMethodContext WithReturnType(Type type,
            Action<ITypeUsageContext>? configure = null)
        {
            context.WithReturnType(t =>
            {
                t.From(type);
                configure?.Invoke(t);
            });
            return context;
        }

        public IMethodContext WithReturnType(INamedTypeSymbol name,
            Action<ITypeUsageContext>? configure = null)
        {
            context.AddUsing(name);
            context.WithReturnType(t =>
            {
                t.From(name);
                configure?.Invoke(t);
            });
            return context;
        }

        public IMethodContext WithReturnType(INameInNamespaceToken name,
            Action<ITypeUsageContext>? configure = null)
        {
            context.AddUsing(name.Namespace);
            context.WithReturnType(t =>
            {
                t.From(name);
                configure?.Invoke(t);
            });
            return context;
        }

        public IMethodContext WithReturnType(IName name,
            Action<ITypeUsageContext>? configure = null)
        {
            context.WithReturnType(t =>
            {
                t.From(name);
                configure?.Invoke(t);
            });
            return context;
        }

        public IMethodContext WithReturnType(string name,
            Action<ITypeUsageContext>? configure = null)
        {
            context.WithReturnType(t =>
            {
                t.From(new PascalCaseName(name));
                configure?.Invoke(t);
            });
            return context;
        }

        public IMethodContext WithAttribute<TAttr>() where TAttr : Attribute =>
            context.WithAttribute(a => a.From<TAttr>());
    }
}