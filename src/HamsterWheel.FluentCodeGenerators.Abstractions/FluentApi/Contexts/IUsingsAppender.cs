using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IUsingsAppender
{
    void AddUsing(INamespace namespaceName);
    void AddStaticUsing(INameInNamespaceToken nameInNamespace);
}