using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ParametersValuesListChunk(IEnumerable<ParameterValueChunk> parameters) : ICodeChunk
{
    private readonly List<ParameterValueChunk> _parameters = [..parameters];

    public IReadOnlyCollection<ParameterValueChunk> Parameters => _parameters.AsReadOnly();

    public void AddParameter(ParameterValueChunk newParameter) => _parameters.Add(newParameter);

    public bool AppendChunks(StringBuilder stringBuilder) =>
        new BracketsChunk(BracesType.Round, new CommaDelimitedJoinChunk(codeChunks: _parameters))
            .AppendChunks(stringBuilder);
}