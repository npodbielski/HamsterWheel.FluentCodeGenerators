namespace HamsterWheel.FluentCodeGenerators;

public interface ISourceCodeFile
{
    string TypeName { get; }
    string Content { get; }
}