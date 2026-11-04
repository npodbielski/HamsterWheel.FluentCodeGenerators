using System.Reflection;
using HamsterWheel.FluentCodeGenerators.External;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public static class IUsingAppenderExtensions
{
    public static TContext WithUsings<TContext>(this TContext context, params string[] namespaces)
        where TContext : IUsingsAppender
    {
        foreach (var ns in namespaces)
        {
            context.AddUsing(Namespace.From(ns));
        }

        return context;
    }

    extension(IUsingsAppender usings)
    {
        public void AddUsing(INameInNamespaceToken typeInfo) =>
            usings.AddUsing(typeInfo.Namespace);

        public void AddUsing<T1>() => usings.AddUsing(typeof(T1));

        public void AddUsing(Type type)
        {
            if (type.GetProperty(nameof(Namespace), BindingFlags.Static | BindingFlags.Public)
                    is { } prop && prop.PropertyType == typeof(Namespace))
            {
                usings.AddUsing(((Namespace)prop.GetValue(null)).ToString());
            }
            else if (type.Namespace != null)
            {
                usings.AddUsing(type.Namespace);
            }
        }

        public void AddUsing(string namespaceName) =>
            usings.AddUsing(namespaceName.ToNamespace());

        public void AddUsing(INamedTypeSymbol typeSymbol) =>
            usings.AddUsing(TypeToken.From(typeSymbol));
    }
}