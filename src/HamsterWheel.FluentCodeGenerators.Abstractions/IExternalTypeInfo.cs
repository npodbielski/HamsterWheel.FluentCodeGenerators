using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators;

public interface IExternalTypeInfo : INameInNamespaceToken
{
    int NumberOfGenericArgs { get; }
}