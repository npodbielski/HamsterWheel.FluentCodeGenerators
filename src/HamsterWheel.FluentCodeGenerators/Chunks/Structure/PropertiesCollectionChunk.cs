using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Structure;

public class PropertiesCollectionChunk(IEnumerable<PropertyDefinitionChunk> propsChunks) : ICodeChunk
{
    private readonly List<PropertyDefinitionChunk> _propertyDefinition = [.. propsChunks];

    public bool AppendChunks(StringBuilder stringBuilder) =>
        new NewLineDelimitedJoinChunk(_propertyDefinition).AppendChunks(stringBuilder);

    public void AddProp(PropertyDefinitionChunk newPropChunk) => _propertyDefinition.Add(newPropChunk);
}