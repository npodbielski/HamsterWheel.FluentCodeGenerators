using System.Text;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class PrimaryConstructorDefinitionChunk(ParameterDefinitionChunk[] parameters) : ICodeChunk
{
    private readonly List<ParameterDefinitionChunk> _parameters = parameters.ToList();
    internal string Name { get; set; } = null!;

    public Dictionary<CamelCaseName, INamedChunk> Parameters => _parameters.ToDictionary(x => x.Name, INamedChunk (x) => x.Type);

    public void AddParameter(ParameterDefinitionChunk newParam)
    {
        if (_parameters.Any(x => x.Name == newParam.Name))
        {
            throw new DuplicatedParameterException(newParam.Name, $".ctor({Name})");
        }

        _parameters.Add(newParam);
    }

    public bool AppendChunks(StringBuilder stringBuilder) =>
        new ParametersDefinitionListChunk(parameters: _parameters).AppendChunks(stringBuilder);
}