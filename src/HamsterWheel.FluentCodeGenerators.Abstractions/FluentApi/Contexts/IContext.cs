using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;

public interface IContext : IUsingsAppender
{
    (IName? firstTypeName, string content) GetCodeFile();
    string BuildFile();
}