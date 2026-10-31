using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class ParameterDefinitionChunk(TypeNameChunk typeChunk, string? name = null, int? order = null)
    : AttributesSetChunk, INamedChunk
{
    private bool _isRef;
    private bool _isOut;
    public CamelCaseName Name { get; private set; } = new(name ?? "");
    IName INamedChunk.Name => Name;
    public TypeNameChunk Type { get; private set; } = typeChunk;

    public int? Order { get; set; } = order;
    private NameInNamespace? _fullType;
    public void SetFullType(NameInNamespace token) => _fullType = token;
    public NameInNamespace GetFullType() => _fullType ?? NameInNamespace.From(Type.Name.ToString(), "");

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
        if (_isRef)
        {
            new RefKeywordChunk().AppendChunks(stringBuilder);
            stringBuilder.AppendSingleSpace();
        }

        if (_isOut)
        {
            new OutKeywordChunk().AppendChunks(stringBuilder);
            stringBuilder.AppendSingleSpace();
        }

        var fullType = GetFullType();
        if (fullType.IsArray)
        {
            Type.MakeArray();
        }

        Type.AppendChunks(stringBuilder);
        stringBuilder.AppendSingleSpace();
        stringBuilder.Append(Name);

        return true;
    }

    //TODO: this should be method of type not parameter
    public void MakeNullable() => Type.MakeNullable();

    public void MakeRef()
    {
        _isRef = true;
        _isOut = false;
    }

    public void MakeOut()
    {
        _isRef = false;
        _isOut = true;
    }

    public void SetNameBasedOnType()
    {
        if (!string.IsNullOrWhiteSpace(Name.ToString()))
        {
            return;
        }

        var parameterName = Type.Name.ToParameterName();
        if (parameterName.ToString() == Type.Name.ToString())
        {
            parameterName = parameterName.Append("Param");
        }

        SetName(parameterName.ToString());
    }

    public void SetType(TypeNameChunk typeNameChunk) => Type = typeNameChunk;
}