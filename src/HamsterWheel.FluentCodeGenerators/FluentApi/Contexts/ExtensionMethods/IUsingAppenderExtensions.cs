using System.Reflection;
using HamsterWheel.FluentCodeGenerators.Tokens;

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

    public static void AddUsing(this IUsingsAppender usings, INameInNamespaceToken typeInfo) =>
        usings.AddUsing(typeInfo.Namespace);

    public static void AddUsing<T1>(this IUsingsAppender usings) => usings.AddUsing(typeof(T1));

    public static void AddUsing(this IUsingsAppender usings, Type type)
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

    public static void AddUsing(this IUsingsAppender usings, string namespaceName) =>
        usings.AddUsing(namespaceName.ToNamespace());
}