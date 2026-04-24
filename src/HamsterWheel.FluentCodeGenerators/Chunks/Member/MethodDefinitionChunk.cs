using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Member;

public class MethodDefinitionChunk(string methodName) : AttributesWithVisibilityChunk(true), ICodeChunk
{
    private readonly List<ParameterDefinitionChunk> _parametersList = [];
    private TypeNameChunk? _returnType;
    private bool _isVirtual = false;
    private bool _isAbstract = false;
    private bool _isOverride;
    private bool _isSealed;
    private bool _isStatic;
    private bool _isPartial;
    private bool _isAsync;
    private bool _isAsyncValueTask;
    private readonly TypeNameChunk[]? _genericParameters = null;
    private string _methodName = methodName;
    private bool _haveExpressionBody;
    private SummaryCommentChunk? _comment;
    public CamelCaseName[] ParameterNames => _parametersList.OrderBy(p => p.Order).Select(p => p.Name).ToArray();

    public MethodBodyChunks BodyChunks { get; } = new();
    public ExpressionBodyChunks ExpressionBodyChunk { get; } = new();
    public void AsExpressionBody() => _haveExpressionBody = true;

    public void ReplaceName(string name) => _methodName = name;

    public void MakeOverride() => _isOverride = true;

    public void MakeSealed() => _isSealed = true;

    public void MakeAsync()
    {
        _isAsync = true;
        _isAsyncValueTask = false;
    }

    public void MakeAsyncValueTask()
    {
        _isAsync = false;
        _isAsyncValueTask = true;
    }

    public void MakeStatic() => _isStatic = true;
    public void MakePartial() => _isPartial = true;

    public void MakeVirtual() => _isVirtual = true;

    public void AddParameter(ParameterDefinitionChunk newParam) => _parametersList.Add(newParam);

    public void SetReturnType(TypeNameChunk type) => _returnType = type;
    public void AddComment(SummaryCommentChunk chunk) => _comment = chunk;

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (_comment?.AppendChunks(stringBuilder) == true)
        {
            stringBuilder.AppendLine();
        }

        if (!_isPartial)
        {
            base.AppendChunks(stringBuilder);
        }

        //TODO: order and validate keywords
        if (_isVirtual)
        {
            new VirtualKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isSealed)
        {
            new SealedKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isOverride)
        {
            new OverrideKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isAbstract)
        {
            new AbstractKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isStatic)
        {
            new StaticKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isPartial)
        {
            new PartialKeywordChunk().AppendChunks(stringBuilder);
        }

        if (_isAsync)
        {
            new AsyncKeywordChunk().AppendChunks(stringBuilder);
            var taskReturnType = TypeNameChunk.From<Task>();
            if (ReturnType != TypeNameChunk.Void)
            {
                var chunk = new TypeNameChunk(ReturnType.Name);
                if (ReturnType.IsNullable)
                {
                    chunk.MakeNullable();
                }

                taskReturnType.AddGenericArgument(chunk);
            }

            _returnType = taskReturnType;
        }     
        
        if (_isAsyncValueTask)
        {
            new AsyncKeywordChunk().AppendChunks(stringBuilder);
            var taskReturnType = TypeNameChunk.From<ValueTask>();
            if (ReturnType != TypeNameChunk.Void)
            {
                var chunk = new TypeNameChunk(ReturnType.Name);
                if (ReturnType.IsNullable)
                {
                    chunk.MakeNullable();
                }

                taskReturnType.AddGenericArgument(chunk);
            }

            _returnType = taskReturnType;
        }

        ReturnType.AppendChunks(stringBuilder);

        stringBuilder.Append($" {_methodName}");
        if (_genericParameters is not null && _genericParameters.Any())
        {
            new BracketsChunk(BracesType.Angle, [.._genericParameters]).AppendChunks(stringBuilder);
        }

        new ParametersDefinitionListChunk(_parametersList.OrderBy(p => p.Order)).AppendChunks(stringBuilder);

        if (!_isPartial)
        {
            if (!_haveExpressionBody)
            {
                BodyChunks.AppendChunks(stringBuilder);
            }
            else
            {
                ExpressionBodyChunk.AppendChunks(stringBuilder);
            }
        }
        else
        {
            stringBuilder.AppendLine(";");
        }

        return true;
    }

    private TypeNameChunk ReturnType => _returnType ?? TypeNameChunk.Void;
}