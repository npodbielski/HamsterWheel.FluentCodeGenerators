using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Member;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class AttributeDefinitionChunk(TypeNameChunk type) : ICodeChunk
{
    private readonly ParametersValuesListChunk _parameters = new([]);
    private bool _isAssemblyAttribute;
    private TypeNameChunk _type = type;

    public void ReplaceType(TypeNameChunk type) => _type = type;

    public void AddParameter(ParameterValueChunk value) => _parameters.AddParameter(value);

    public void MakeAssemblyAttribute() => _isAssemblyAttribute = true;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        var child = new CtorCallChunk(_type, _parameters, false);
        ICodeChunk[] children = _isAssemblyAttribute ? [new PlainValueChunk("assembly: "), child] : [child];
        var bracketsChunk = new BracketsChunk(BracesType.Square, children);
        bracketsChunk.AppendChunks(stringBuilder);

        return true;
    }

    public static AttributeDefinitionChunk From(string typeName)
    {
        const string attributeName = nameof(Attribute);
        if (typeName.EndsWith(attributeName))
        {
            var attrLength = attributeName.Length;
            typeName = typeName[..^attrLength];
        }

        var typeChunk = new TypeNameChunk(typeName);
        return new AttributeDefinitionChunk(typeChunk);
    }

    public static AttributeDefinitionChunk Empty() => new(new TypeNameChunk(nameof(Attribute)));

    public static AttributeDefinitionChunk AssemblyAttribute()
    {
        var attribute = new AttributeDefinitionChunk(new TypeNameChunk(nameof(Attribute)));
        attribute.MakeAssemblyAttribute();
        return attribute;
    }
}