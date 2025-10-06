namespace HamsterWheel.FluentCodeGenerators.Tokens;

public interface INameInNamespaceToken
{
    PascalCaseName Name { get; }
    Namespace Namespace { get; }
}