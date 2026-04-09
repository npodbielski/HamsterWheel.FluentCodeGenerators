using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Structure;
using HamsterWheel.FluentCodeGenerators.Chunks.Syntax;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks;

public class CodeFileChunks : ICodeChunk
{
    public UsingStatementsListChunk Usings { get; } = new();
    private readonly List<ICodeChunk> _assemblyAttributes = [];
    private readonly List<ICodeChunk> _types = [];
    private readonly List<ICodeChunk> _pragmas = [];
    private readonly List<ICodeChunk> _aliases = [];
    private FileScopeNamespaceChunk? _fileScopeNamespace;

    internal IName? FirstTypeName => _types.OfType<INamedChunk>().Select(t => t.Name).FirstOrDefault();

    public void AddType(ICodeChunk type) => _types.Add(type);

    public void AddPragma(ICodeChunk pragma) => _pragmas.Add(pragma);
    public void AddAlias(ICodeChunk alias) => _aliases.Add(alias);

    public void AddAssemblyAttribute(AttributeDefinitionChunk attr) => _assemblyAttributes.Add(attr);

    public void AddFileScopeNamespace(FileScopeNamespaceChunk chunk) => _fileScopeNamespace = chunk;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (_pragmas.Aggregate(false, (current, codeBuilder) => current | codeBuilder.AppendChunks(stringBuilder)))
        {
            stringBuilder.AppendLine();
        }
        
        if (_aliases.Aggregate(false, (current, codeBuilder) => current | codeBuilder.AppendChunks(stringBuilder)))
        {
            stringBuilder.AppendLine();
        }

        //to allow manipulating of the usings collection inside the class code (i.e., in method body), this needs to be written at the end so just we just need dummy string to replace later 
        stringBuilder.AppendLine("//USINGS HERE");

        if (_assemblyAttributes.Aggregate(false, (c, chunk) => c | chunk.AppendChunks(stringBuilder)))
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        if (_fileScopeNamespace is not null)
        {
            _fileScopeNamespace.AppendChunks(stringBuilder);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        new DoubleNewLineDelimitedJoinChunk(_types).AppendChunks(stringBuilder);

        var usingsStringBuilder = new StringBuilder();

        if (Usings.AppendChunks(usingsStringBuilder))
        {
            stringBuilder.AppendLine();
        }

        //using are replaced as last step to make sure that all the nested chunks pushed their types namespaces to usings collection
        stringBuilder.Replace("//USINGS HERE", usingsStringBuilder.ToString());

        return true;
    }

    public static implicit operator string(CodeFileChunks chunks)
    {
        var stringBuilder = new StringBuilder();
        chunks.AppendChunks(stringBuilder);
        return stringBuilder.ToString();
    }
}