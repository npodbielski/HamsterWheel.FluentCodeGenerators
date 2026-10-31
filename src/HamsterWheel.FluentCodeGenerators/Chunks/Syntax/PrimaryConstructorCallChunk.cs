using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class PrimaryConstructorCallChunk : ICodeChunk
{
    public List<ParameterValueChunk> Parameters { get; } = [];

    public void AddParameter(ParameterValueChunk newParam) => Parameters.Add(newParam);

    public bool AppendChunks(StringBuilder stringBuilder) =>
        new ParametersValuesListChunk(parameters: Parameters).AppendChunks(stringBuilder);
}