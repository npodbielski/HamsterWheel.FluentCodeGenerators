using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ParametersDefinitionListChunk(IEnumerable<ParameterDefinitionChunk> parameters) : ICodeChunk
{
    public CamelCaseName[] ParameterNames => _parameters.OrderBy(p => p.Order).Select(p => p.Name).ToArray();
    private readonly List<ParameterDefinitionChunk> _parameters = [..parameters];

    public void Add(ParameterDefinitionChunk parameter) => _parameters.Add(parameter);

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        return new BracketsChunk(
            BracesType.Round, new CommaDelimitedJoinChunk(_parameters)).AppendChunks(stringBuilder);
    }
}