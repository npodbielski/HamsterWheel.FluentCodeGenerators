using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.FluentApi.Contexts.Base;

public abstract class CodeBuilderContextBase(CodeFileChunks fileChunks) : IContext, INullabilitySettings
{
    public FluentApiSettingsSnapshot Settings { get; private set; } = FluentApiSettings.Snapshot();

    private readonly INullabilitySettings _nullabilitySettings = new NullabilitySettings();
    internal IUsingsAppender UsingsAppender => this;
    internal UsingStatementsListChunk Usings => FileChunks.Usings;
    protected CodeFileChunks FileChunks => fileChunks;

    protected CodeBuilderContextBase(CodeBuilderContextBase previous) : this(previous.FileChunks)
    {
        _nullabilitySettings = previous._nullabilitySettings;
        Settings = previous.Settings;
    }

    public string BuildFile() => FileChunks;

    public (IName? firstTypeName, string content) GetCodeFile() => (FileChunks.FirstTypeName, FileChunks);

    bool INullabilitySettings.Enabled
    {
        get => _nullabilitySettings.Enabled;
        set => _nullabilitySettings.Enabled = value;
    }

    void IUsingsAppender.AddUsing(INamespace namespaceName) => Usings.Append(namespaceName);

    void IUsingsAppender.AddStaticUsing(INameInNamespaceToken nameInNamespace) => Usings.AppendStatic(nameInNamespace);
}