using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ParameterDefinitionChunk(TypeNameChunk typeChunk, string? name = null, int? order = null)
    : AttributesSetChunk, INamedChunk
{
    public CamelCaseName Name { get; private set; } = new(name ?? "");
    IName INamedChunk.Name => Name;
    public TypeNameChunk Type { get; private set; } = typeChunk;
    public string NameAsString => Name;

    public int? Order { get; set; } = order;

    public ParameterDefinitionChunk() : this(TypeNameChunk.String)
    {
    }

    public void SetName(string newName) => Name = newName.ToPascalCaseName().ToParameterName();

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (Name.ToString() == "")
        {
            SetNameBasedOnType();
        }

        base.AppendChunks(stringBuilder);
        Type.AppendChunks(stringBuilder);
        stringBuilder.AppendSingleSpace();
        stringBuilder.Append(Name);

        return true;
    }

    //TODO: this should be method of type not parameter
    public void MakeNullable() => Type.MakeNullable();

    public void SetNameBasedOnType()
    {
        if (string.IsNullOrWhiteSpace(Name.ToString()))
        {
            var parameterName = Type.Name.ToParameterName();
            if (parameterName.ToString() == Type.Name.ToString())
            {
                parameterName = parameterName.Append("Param");
            }

            SetName(parameterName.ToString());
        }
    }

    public void SetType(TypeNameChunk typeNameChunk) => Type = typeNameChunk;
}