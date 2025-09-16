using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class FieldDefinitionChunk(string name) : ICodeChunk
{
    private bool _isStatic;
    private string? _name = name;
    private TypeNameChunk _type = new("object");
    private MemberVisibility _visibility = MemberVisibility.Private;
    private AttributesDefinitionChunk? _attributes = null;
    private InitializerChunk? _initializer;

    public void ReplaceName(IName name) => _name = name.ToString();

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        _attributes?.AppendChunks(stringBuilder);
        new VisibilityKeywordCodeChunk(_visibility).AppendChunks(stringBuilder);
        if (_isStatic)
        {
            new StaticKeywordChunk().AppendChunks(stringBuilder);
        }

        _type.AppendChunks(stringBuilder);
        stringBuilder.Append($" {_name}");
        if (_initializer is not null)
        {
            stringBuilder.AppendSingleSpace();
            _initializer.AppendChunks(stringBuilder);
        }
        else
        {
            stringBuilder.Append(";");
        }

        return true;
    }

    public static FieldDefinitionChunk From(string name) => new(name);

    public void MakeNullable()
    {
        _type.MakeNullable();
    }

    public void DisableNullableWarning()
    {
        _initializer = new DisableNullabilityWarningChunk();
    }

    public void WithInitializer(string value)
    {
        _initializer = new InitializerChunk(value);
    }

    public void SetType(TypeNameChunk typeNameChunk)
    {
        _type = typeNameChunk;
    }

    public void MakeStatic()
    {
        _isStatic = true;
    }

    public void SetVisibility(MemberVisibility value) => _visibility = value;
}