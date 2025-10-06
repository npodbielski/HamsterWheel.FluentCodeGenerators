using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class CtorDefinitionChunk(
    MemberVisibility visibility = MemberVisibility.Public,
    ParametersDefinitionListChunk? ctorParams = null,
    ICodeChunk? chainedCtor = null)
    : ICodeChunk
{
    private bool _haveExpressionBody;

    private readonly ParametersDefinitionListChunk _ctorParametersChunk =
        ctorParams ?? new ParametersDefinitionListChunk([]);

    public CamelCaseName[] ParametersNames => _ctorParametersChunk.ParameterNames;
    public ExpressionBodyChunks ExpressionBodyChunk { get; } = new();
    private string? TypeName { get; set; }
    public void AsExpressionBody() => _haveExpressionBody = true;
    public MethodBodyChunks BodyChunks { get; } = new();

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        new VisibilityKeywordCodeChunk(visibility).AppendChunks(stringBuilder);
        stringBuilder.Append(TypeName);
        _ctorParametersChunk.AppendChunks(stringBuilder);
        if (chainedCtor is not null)
        {
            stringBuilder.Append(" : ");
            chainedCtor.AppendChunks(stringBuilder);
        }

        if (_haveExpressionBody)
        {
            ExpressionBodyChunk.AppendChunks(stringBuilder);
        }
        else
        {
            BodyChunks.AppendChunks(stringBuilder);
        }

        return true;
    }

    public void SetName(TypeDefinitionWithPrimaryConstructorChunk name) => TypeName = name.TypeName;

    public void AddParameter(ParameterDefinitionChunk newParam) => _ctorParametersChunk.Add(newParam);
}